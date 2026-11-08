// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Query;

namespace Jeebs.Data.Repository.FluentQuery_Tests;

public class Count_Tests : FluentQuery_Tests
{
	[Fact]
	public async Task Query_Errors__Returns_Fail__Does_Not_Call_Db()
	{
		// Arrange
		var (query, v) = Setup();
		var withWhere = query.Where(Rnd.Str, Compare.Equal, Rnd.Str);

		// Act
		var result = await withWhere.CountAsync();

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(withWhere);
		_ = result.AssertFailure("Query errors.", fluent.Errors);
		await v.Db.DidNotReceiveWithAnyArgs().QuerySingleAsync<long>(default!, default!);
	}

	[Fact]
	public async Task No_Predicates__Returns_Fail()
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var result = await query.CountAsync();

		// Assert
		_ = result.AssertFailure("No predicates defined for WHERE clause.");
	}

	[Fact]
	public async Task Calls_Client_GetCountQuery__With_Parts()
	{
		// Arrange
		var (query, v) = Setup();
		var withWhere = WithWhere(query);

		// Act
		_ = await withWhere.CountAsync();

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(withWhere);
		v.Client.Received(1).GetCountQuery(fluent.Parts);
	}

	[Fact]
	public async Task Calls_Db_QuerySingleAsync__With_Correct_Values()
	{
		// Arrange
		var sql = Rnd.Str;
		var param = Substitute.For<IQueryParametersDictionary>();
		var (query, v) = Setup(sql, param);

		// Act
		_ = await WithWhere(query).CountAsync();

		// Assert
		await v.Db.Received(1).QuerySingleAsync<long>(sql, param);
	}

	private static IFluentQuery<TestEntity, TestId> WithWhere(FluentQuery<TestEntity, TestId> query) =>
		query.Update(parts => parts with
		{
			WhereCustom = parts.WhereCustom.WithItem((Rnd.Str, new QueryParametersDictionary()))
		});
}
