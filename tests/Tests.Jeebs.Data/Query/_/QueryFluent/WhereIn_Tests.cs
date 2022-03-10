﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Query.QueryFluent_Tests;

public class WhereIn_Tests : QueryFluent_Tests
{
	[Fact]
	public void No_Values_Does_Not_Add_Predicate()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var result = query.WhereIn(x => x.Foo, Array.Empty<string?>());

		// Assert
		var fluent = Assert.IsType<QueryFluent<TestEntity, TestId>>(result);
		Assert.Empty(fluent.Predicates);
	}

	[Fact]
	public void Adds_Predicate()
	{
		// Arrange
		var (query, v) = Setup();
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;

		// Act
		var result = query.WhereIn(x => x.Foo, new[] { v0, v1 });

		// Assert
		var fluent = Assert.IsType<QueryFluent<TestEntity, TestId>>(result);
		Assert.Collection(fluent.Predicates, x =>
		{
			Assert.Equal(Compare.In, x.cmp);
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