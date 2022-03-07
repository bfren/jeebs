// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Enums;
using NSubstitute;
using Xunit;
using static Jeebs.Data.Querying.QueryFluent.M;

namespace Jeebs.Data.Querying.QueryFluent_Tests;

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
		_ = Assert.IsType<NoPredicatesMsg>(none);
	}

	[Fact]
	public async Task Calls_QueryAsync_With_Predicates()
	{
		// Arrange
		var (query, v) = Setup();
		var predicates = Substitute.For<IImmutableList<(Expression<Func<TestEntity, object>>, Compare, object)>>();
		_ = predicates.Count.Returns(1);

		// Act
		_ = await (query with { Predicates = predicates }).QueryAsync<int>().ConfigureAwait(false);

		// Assert
		var array = predicates.Received().ToArray();
		_ = await v.Repo.Received().QueryAsync<int>(array).ConfigureAwait(false);
	}
}
