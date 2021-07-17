// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Enums;
using NSubstitute;
using Xunit;
using static Jeebs.Data.Querying.QueryFluent.Msg;

namespace Jeebs.Data.Querying.QueryFluent_Tests
{
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
			_ = await (query with { Predicates = predicates }).QuerySingleAsync<int>();

			// Assert
			var array = predicates.Received().ToArray();
			await v.Repo.Received().QuerySingleAsync<int>(array);
		}
	}
}
