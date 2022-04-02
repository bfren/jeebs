// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using static Jeebs.Data.Query.QueryFluent.M;

namespace Jeebs.Data.Query.QueryFluent_Tests;

public class QuerySingleAsync_Tests : QueryFluent_Tests
{
	[Fact]
	public async Task No_Predicates_Returns_None_With_NoPredicatesMsg()
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var result = await query.QuerySingleAsync<int>();

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
		await (query with { Predicates = predicates }).QuerySingleAsync<int>();
		await (query with { Predicates = predicates }).QuerySingleAsync<int>(v.Transaction);

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
		await (query with { Predicates = predicates }).QuerySingleAsync<int>();
		await (query with { Predicates = predicates }).QuerySingleAsync<int>(v.Transaction);

		// Assert
		await v.Db.Received().QueryAsync<int>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>());
		await v.Db.Received().QueryAsync<int>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, v.Transaction);
	}

	[Fact]
	public async Task Unwraps_Single_Value()
	{
		// Arrange
		var (query, v) = Setup();
		var predicates = Substitute.For<IImmutableList<(string, Compare, dynamic)>>();
		predicates.Count.Returns(1);
		var value = Rnd.Int;
		v.Db.QueryAsync<int>(default!, default, default, default!)
			.ReturnsForAnyArgs(new[] { value });

		// Act
		var r0 = await (query with { Predicates = predicates }).QuerySingleAsync<int>();
		var r1 = await (query with { Predicates = predicates }).QuerySingleAsync<int>(v.Transaction);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(value, s1);
	}
}
