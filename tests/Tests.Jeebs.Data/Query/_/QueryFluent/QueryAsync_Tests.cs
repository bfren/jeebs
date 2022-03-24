// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using static Jeebs.Data.Query.QueryFluent.M;

namespace Jeebs.Data.Query.QueryFluent_Tests;

public class QueryAsync_Tests : QueryFluent_Tests
{
	[Fact]
	public async Task No_Predicates_Returns_None_With_NoPredicatesMsg()
	{
		// Arrange
		var (query, _) = Setup();

		// Act
		var result = await query.QueryAsync<int>().ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<NoPredicatesMsg>(none);
	}

	[Fact]
	public async Task Calls_QueryAsync_With_Predicates()
	{
		// Arrange
		var (query, v) = Setup();
		var predicates = Substitute.For<IImmutableList<(Expression<Func<TestEntity, object>>, Compare, object)>>();
		predicates.Count.Returns(1);

		// Act
		await (query with { Predicates = predicates }).QueryAsync<int>().ConfigureAwait(false);

		// Assert
		var array = predicates.Received().ToArray();
		await v.Repo.Received().QueryAsync<int>(array).ConfigureAwait(false);
	}
}
