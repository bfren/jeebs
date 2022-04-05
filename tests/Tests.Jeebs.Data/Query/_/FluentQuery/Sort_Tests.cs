// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using MaybeF;
using static MaybeF.F.EnumerableF.M;

namespace Jeebs.Data.Query.FluentQuery_Tests;

public class Sort_Tests : FluentQuery_Tests
{
	[Fact]
	public void Query_Errors__Does_Not_Add_Sort__Returns_Original_Query()
	{
		// Arrange
		var (query, v) = Setup();
		query.Errors.Add(Substitute.For<IMsg>());

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
		Assert.Collection(f0.Parts.Sort, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Foo, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Foo), x.column.ColAlias);
			Assert.Equal(order, x.order);
		});
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		Assert.Collection(f1.Parts.Sort, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Foo, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Foo), x.column.ColAlias);
			Assert.Equal(order, x.order);
		});
	}

	[Theory]
	[InlineData(SortOrder.Ascending)]
	[InlineData(SortOrder.Descending)]
	public void Unable_To_Get_Column__Adds_Error__Returns_Original_Query(SortOrder order)
	{
		// Arrange
		var (query, v) = Setup();
		var column = Rnd.Str;

		// Act
		var result = query.Sort(column, order);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Collection(fluent.Errors,
			x => x.AssertType<NoMatchingItemsMsg>()
		);
		Assert.Same(query, fluent);
	}
}
