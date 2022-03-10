﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Query.QueryFluent_Tests;

public class Where_Tests : QueryFluent_Tests
{
	[Fact]
	public void Null_Value_Does_Not_Add_Predicate()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var result = query.Where(x => x.Foo, Compare.Like, null);

		// Assert
		var fluent = Assert.IsType<QueryFluent<TestEntity, TestId>>(result);
		Assert.Empty(fluent.Predicates);
	}

	[Theory]
	[InlineData(Compare.Equal)]
	[InlineData(Compare.NotEqual)]
	[InlineData(Compare.Like)]
	[InlineData(Compare.LessThan)]
	[InlineData(Compare.LessThanOrEqual)]
	[InlineData(Compare.MoreThan)]
	[InlineData(Compare.MoreThanOrEqual)]
	public void Adds_Predicate(Compare cmp)
	{
		// Arrange
		var (query, v) = Setup();
		var value = Rnd.Str;

		// Act
		var result = query.Where(x => x.Foo, cmp, value);

		// Assert
		var fluent = Assert.IsType<QueryFluent<TestEntity, TestId>>(result);
		Assert.Collection(fluent.Predicates, x =>
		{
			Assert.Equal(cmp, x.cmp);
			Assert.Equal(value, x.val);
		});
	}
}