// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using Jeebs.Data.Enums;
using Xunit;

namespace Jeebs.Data.Querying.QueryFluent_Tests
{
	public class WhereNotIn_Tests : QueryFluent_Tests
	{
		[Fact]
		public void No_Values_Does_Not_Add_Predicate()
		{
			// Arrange
			var (query, v) = Setup();

			// Act
			var result = query.WhereNotIn(x => x.Foo, Array.Empty<string?>());

			// Assert
			var fluent = Assert.IsType<QueryFluent<TestEntity, TestId>>(result);
			Assert.Empty(fluent.Predicates);
		}

		[Fact]
		public void Adds_Predicate()
		{
			// Arrange
			var (query, v) = Setup();
			var v0 = F.Rnd.Str;
			var v1 = F.Rnd.Str;

			// Act
			var result = query.WhereNotIn(x => x.Foo, new[] { v0, v1 });

			// Assert
			var fluent = Assert.IsType<QueryFluent<TestEntity, TestId>>(result);
			Assert.Collection(fluent.Predicates, x =>
			{
				Assert.Equal(Compare.NotIn, x.cmp);
				var coll = Assert.IsAssignableFrom<IEnumerable<object>>(x.val);
				Assert.Collection(coll,
					y =>
					{
						Assert.Equal(v0, y);
					},
					y =>
					{
						Assert.Equal(v1, y);
					}
				);
			});
		}
	}
}
