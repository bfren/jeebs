// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Query.FluentQuery_Tests;

public class Where_Tests : FluentQuery_Tests
{
	[Fact]
	public void Null_Value__Does_Not_Add_Predicate()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var r0 = query.Where(nameof(TestEntity.Foo), Compare.Like, null);
		var r1 = query.Where(x => x.Foo, Compare.Like, null);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		Assert.Empty(f0.Parts.Where);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		Assert.Empty(f1.Parts.Where);
	}

	[Theory]
	[InlineData(Compare.Equal)]
	[InlineData(Compare.NotEqual)]
	[InlineData(Compare.Like)]
	[InlineData(Compare.LessThan)]
	[InlineData(Compare.LessThanOrEqual)]
	[InlineData(Compare.MoreThan)]
	[InlineData(Compare.MoreThanOrEqual)]
	public void Valid_Value__Adds_Predicate(Compare compare)
	{
		// Arrange
		var (query, v) = Setup();
		var value = Rnd.Str;

		// Act
		var r0 = query.Where(nameof(TestEntity.Foo), compare, value);
		var r1 = query.Where(x => x.Foo, compare, value);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		Assert.Collection(f0.Parts.Where, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Foo, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Foo), x.column.ColAlias);
			Assert.Equal(compare, x.compare);
			Assert.Equal(value, x.value);
		});
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		Assert.Collection(f1.Parts.Where, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Foo, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Foo), x.column.ColAlias);
			Assert.Equal(compare, x.compare);
			Assert.Equal(value, x.value);
		});
	}
}
