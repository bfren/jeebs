// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Repository.FluentQuery_Tests;

public class Where_Tests1 : FluentQuery_Tests
{
	[Fact]
	public void Query_Errors__Does_Not_Add_Predicate__Returns_Original_Query()
	{
		// Arrange
		var (query, v) = Setup();
		query.Errors.Add(FailGen.Create().Value);

		// Act
		var r0 = query.Where(nameof(TestEntity.Foo), Compare.Like, Rnd.Str);
		var r1 = query.Where(x => x.Foo, Compare.Like, Rnd.Str);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		Assert.Empty(f0.Parts.Where);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		Assert.Empty(f1.Parts.Where);
		Assert.Same(query, f0);
		Assert.Same(query, f1);
	}

	[Theory]
	[InlineData(Compare.Is)]
	[InlineData(Compare.IsNot)]
	public void Null_Value__Adds_Predicate_With_DBNull(Compare compare)
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var r0 = query.Where(nameof(TestEntity.Foo), compare, null);
		var r1 = query.Where(x => x.Foo, compare, null);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		var s0 = Assert.Single(f0.Parts.Where);
		Assert.Equal(v.Table.GetName(), s0.column.TblName);
		Assert.Equal(v.Table.Foo, s0.column.ColName);
		Assert.Equal(nameof(TestEntity.Foo), s0.column.ColAlias);
		Assert.Equal(compare, s0.compare);
		Assert.IsType<DBNull>(s0.value);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		var s1 = Assert.Single(f1.Parts.Where);
		Assert.Equal(v.Table.GetName(), s1.column.TblName);
		Assert.Equal(v.Table.Foo, s1.column.ColName);
		Assert.Equal(nameof(TestEntity.Foo), s1.column.ColAlias);
		Assert.Equal(compare, s1.compare);
		Assert.IsType<DBNull>(s1.value);
	}

	[Theory]
	[InlineData(Compare.Equal)]
	[InlineData(Compare.NotEqual)]
	[InlineData(Compare.Like)]
	[InlineData(Compare.LessThan)]
	[InlineData(Compare.LessThanOrEqual)]
	[InlineData(Compare.MoreThan)]
	[InlineData(Compare.MoreThanOrEqual)]
	[InlineData(Compare.Is)]
	[InlineData(Compare.IsNot)]
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
		var s0 = Assert.Single(f0.Parts.Where);
		Assert.Equal(v.Table.GetName(), s0.column.TblName);
		Assert.Equal(v.Table.Foo, s0.column.ColName);
		Assert.Equal(nameof(TestEntity.Foo), s0.column.ColAlias);
		Assert.Equal(compare, s0.compare);
		Assert.Equal(value, s0.value);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		var s1 = Assert.Single(f1.Parts.Where);
		Assert.Equal(v.Table.GetName(), s1.column.TblName);
		Assert.Equal(v.Table.Foo, s1.column.ColName);
		Assert.Equal(nameof(TestEntity.Foo), s1.column.ColAlias);
		Assert.Equal(compare, s1.compare);
		Assert.Equal(value, s1.value);
	}

	[Fact]
	public void Unable_To_Get_Column__Adds_Error__Returns_Original_Query()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var result = query.Where(Rnd.Str, Compare.LessThan, Rnd.Guid);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		var single = Assert.Single(fluent.Errors);
		Assert.Equal("Column with alias '{Alias}' not found in table '{Table}'.", single.Message);
		Assert.Same(query, fluent);
	}
}
