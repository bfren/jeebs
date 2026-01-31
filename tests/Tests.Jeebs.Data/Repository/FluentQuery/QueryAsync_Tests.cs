// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;

namespace Jeebs.Data.Repository.FluentQuery_Tests;

public class QueryAsync_Tests : FluentQuery_Tests
{
	[Fact]
	public async Task Query_Errors__Returns_None_With_ListMsg()
	{
		// Arrange
		var (query, v) = Setup();
		var m0 = FailGen.Create().Value;
		var m1 = FailGen.Create().Value;
		query.Errors.Add(m0);
		query.Errors.Add(m1);

		// Act
		var result = await query.QueryAsync<int>();

		// Assert
		_ = result.AssertFailure("Query errors.", query.Errors);
	}

	[Fact]
	public async Task No_Predicates__Returns_Fail()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var result = await query.QueryAsync<string>();

		// Assert
		_ = result.AssertFailure("No predicates defined for WHERE clause.");
	}

	[Fact]
	public async Task Calls_Db_Client_GetQuery__With_Parts()
	{
		// Arrange
		var (query, v) = Setup();
		var withWhere = query.Update(parts => parts with
		{
			WhereCustom = parts.WhereCustom.WithItem((Rnd.Str, new QueryParametersDictionary()))
		});

		// Act
		_ = await withWhere.QueryAsync<string>();

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(withWhere);
		v.Client.Received(1).GetQuery(fluent.Parts);
	}

	[Fact]
	public async Task Calls_Db_QueryAsync__With_Correct_Values()
	{
		// Arrange
		var sql = Rnd.Str;
		var param = new QueryParametersDictionary();
		var (query, v) = Setup(sql, param);
		var withWhere = query.Update(parts => parts with
		{
			WhereCustom = parts.WhereCustom.WithItem((Rnd.Str, new QueryParametersDictionary()))
		});

		// Act
		_ = await withWhere.QueryAsync<string>();

		// Assert
		Assert.IsType<FluentQuery<TestEntity, TestId>>(withWhere);
		await v.Db.Received(1).QueryAsync<string>(sql, param);
	}
}
