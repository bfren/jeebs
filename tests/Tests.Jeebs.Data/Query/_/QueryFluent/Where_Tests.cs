// Jeebs Unit Tests
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
		var r0 = query.Where(nameof(TestEntity.Foo), Compare.Like, null);
		var r1 = query.Where(x => x.Foo, Compare.Like, null);

		// Assert
		var f0 = Assert.IsType<QueryFluent<TestEntity, TestId>>(r0);
		Assert.Empty(f0.Predicates);
		var f1 = Assert.IsType<QueryFluent<TestEntity, TestId>>(r1);
		Assert.Empty(f1.Predicates);
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
		var r0 = query.Where(nameof(TestEntity.Foo), cmp, value);
		var r1 = query.Where(x => x.Foo, cmp, value);

		// Assert
		var f0 = Assert.IsType<QueryFluent<TestEntity, TestId>>(r0);
		Assert.Collection(f0.Predicates, x =>
		{
			Assert.Equal(cmp, x.cmp);
			Assert.Equal(value, x.val);
		});
		var f1 = Assert.IsType<QueryFluent<TestEntity, TestId>>(r1);
		Assert.Collection(f1.Predicates, x =>
		{
			Assert.Equal(cmp, x.cmp);
			Assert.Equal(value, x.val);
		});
	}
}
