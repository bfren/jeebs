// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using static Jeebs.Data.Query.FluentQuery.M;

namespace Jeebs.Data.Query.FluentQuery_Tests;

public class QueryAsync_Tests : QueryFluent_Tests
{
	[Fact]
	public async Task No_Predicates_Returns_None_With_NoPredicatesMsg()
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var result = await query.QueryAsync<int>();

		// Assert
		result.AssertNone().AssertType<NoPredicatesMsg>();
	}

	[Fact]
	public async Task Calls_Db_Client_GetQuery_With_Predicates()
	{
		// Arrange
		var (query, v) = Setup();
		var predicates = Substitute.For<IImmutableList<(string, Compare, dynamic)>>();
		predicates.Count.Returns(1);

		// Act
		await (query with { Predicates = predicates }).QueryAsync<int>();
		await (query with { Predicates = predicates }).QueryAsync<int>(v.Transaction);

		// Assert
		var array = predicates.Received().ToArray();
		v.Client.Received(2).GetQuery<TestEntity, int>(array);
	}

	[Fact]
	public async Task Calls_Db_QueryAsync()
	{
		// Arrange
		var (query, v) = Setup();
		var predicates = Substitute.For<IImmutableList<(string, Compare, dynamic)>>();
		predicates.Count.Returns(1);

		// Act
		await (query with { Predicates = predicates }).QueryAsync<int>();
		await (query with { Predicates = predicates }).QueryAsync<int>(v.Transaction);

		// Assert
		await v.Db.Received().QueryAsync<int>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>());
		await v.Db.Received().QueryAsync<int>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, v.Transaction);
	}
}
