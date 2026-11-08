// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Data.Query;

namespace Jeebs.Data.Db_Tests;

public class QueryAsync_Tests : Db_Setup
{
	[Fact]
	public async Task With_Page__Counts_Items__Then_Queries_Requested_Page()
	{
		// Arrange
		var (db, v) = Setup();
		var parts = new QueryParts(Substitute.For<ITable>()) { Maximum = 10 };
		var (countSql, countParam) = (Rnd.Str, new QueryParametersDictionary());
		var (itemsSql, itemsParam) = (Rnd.Str, new QueryParametersDictionary());
		var items = new[] { Rnd.Str, Rnd.Str };
		v.Client.GetCountQuery(parts).Returns((countSql, countParam));
		v.Client.GetQuery(Arg.Any<IQueryParts>()).Returns((itemsSql, itemsParam));
		db.QuerySingleAsync<ulong>(countSql, countParam).Returns(25UL);
		db.QueryAsync<string>(itemsSql, itemsParam).Returns(items);

		// Act
		var result = await db.QueryAsync<string>(2, parts);

		// Assert
		var list = result.AssertOk();
		Assert.Equal(items, list);
		Assert.Equal(25UL, list.Values.Items);
		Assert.Equal(2UL, list.Values.Page);
		Assert.Equal(3UL, list.Values.Pages);
		v.Client.Received(1).GetQuery(Arg.Is<IQueryParts>(x => x.Skip == 10 && x.Maximum == 10));
	}

	[Fact]
	public async Task With_Page__Count_Fails__Returns_Failure__Does_Not_Query_Items()
	{
		// Arrange
		var (db, v) = Setup();
		var parts = new QueryParts(Substitute.For<ITable>());
		var (countSql, countParam) = (Rnd.Str, new QueryParametersDictionary());
		var failure = FailGen.Create<ulong>();
		v.Client.GetCountQuery(parts).Returns((countSql, countParam));
		db.QuerySingleAsync<ulong>(countSql, countParam).Returns(failure);

		// Act
		var result = await db.QueryAsync<string>(1, parts);

		// Assert
		_ = result.AssertFailure();
		v.Client.DidNotReceive().GetQuery(Arg.Any<IQueryParts>());
	}
}
