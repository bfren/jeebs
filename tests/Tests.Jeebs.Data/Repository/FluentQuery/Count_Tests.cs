// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;

namespace Jeebs.Data.Repository.FluentQuery_Tests;

public class Count_Tests : FluentQuery_Tests
{
	[Fact]
	public async Task Calls_Client_GetCountQuery__With_Parts()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		_ = await query.CountAsync();

		// Assert
		v.Client.Received(1).GetCountQuery(query.Parts);
	}

	[Fact]
	public async Task Calls_Db_QuerySingleAsync__With_Correct_Values()
	{
		// Arrange
		var sql = Rnd.Str;
		var param = Substitute.For<IQueryParametersDictionary>();
		var (query, v) = Setup(sql, param);

		// Act
		_ = await query.CountAsync();

		// Assert
		await v.Db.Received(1).QuerySingleAsync<long>(sql, param);
	}
}
