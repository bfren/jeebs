// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Repository.FluentQuery_Tests;

public class Sort_Tests : FluentQuery_Tests
{
	[Fact]
	public void Query_Errors__Does_Not_Add_Sort__Returns_Original_Query()
	{
		// Arrange
		var (query, v) = Setup();
		query.Errors.Add(FailGen.Create().Value);

		// Act
		var r0 = query.Sort(nameof(TestEntity.Foo), SortOrder.Ascending);
		var r1 = query.Sort(x => x.Foo, SortOrder.Descending);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		Assert.Empty(f0.Parts.Sort);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		Assert.Empty(f1.Parts.Sort);
		Assert.Same(query, f0);
		Assert.Same(query, f1);
	}

	[Theory]
	[InlineData(SortOrder.Ascending)]
	[InlineData(SortOrder.Descending)]
	public void Valid_Value__Adds_Sort(SortOrder order)
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var r0 = query.Sort(nameof(TestEntity.Foo), order);
		var r1 = query.Sort(x => x.Foo, order);

		// Assert
		var f0 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r0);
		var s0 = Assert.Single(f0.Parts.Sort);
		Assert.Equal(v.Table.GetName(), s0.column.TblName);
		Assert.Equal(v.Table.Foo, s0.column.ColName);
		Assert.Equal(nameof(TestEntity.Foo), s0.column.ColAlias);
		Assert.Equal(order, s0.order);
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		var s1 = Assert.Single(f1.Parts.Sort);
		Assert.Equal(v.Table.GetName(), s1.column.TblName);
		Assert.Equal(v.Table.Foo, s1.column.ColName);
		Assert.Equal(nameof(TestEntity.Foo), s1.column.ColAlias);
		Assert.Equal(order, s1.order);
	}

	[Theory]
	[InlineData(SortOrder.Ascending)]
	[InlineData(SortOrder.Descending)]
	public void Unable_To_Get_Column__Adds_Error__Returns_Original_Query(SortOrder order)
	{
		// Arrange
		var (query, _) = Setup();
		var column = Rnd.Str;

		// Act
		var result = query.Sort(column, order);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		var single = Assert.Single(fluent.Errors);
		Assert.Equal("Column with alias '{Alias}' not found in table '{Table}'.", single.Message);
		Assert.Same(query, fluent);
	}
}
