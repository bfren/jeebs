// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using MaybeF;
using static MaybeF.F.EnumerableF.M;

namespace Jeebs.Data.Query.FluentQuery_Tests;

public class Where_Tests1 : FluentQuery_Tests
{
	[Fact]
	public void Query_Errors__Does_Not_Add_Predicate__Returns_Original_Query()
	{
		// Arrange
		var (query, v) = Setup();
		query.Errors.Add(Substitute.For<IMsg>());

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
		Assert.Collection(f0.Parts.Where, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Foo, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Foo), x.column.ColAlias);
			Assert.Equal(compare, x.compare);
			Assert.IsType<DBNull>(x.value);
		});
		var f1 = Assert.IsType<FluentQuery<TestEntity, TestId>>(r1);
		Assert.Collection(f1.Parts.Where, x =>
		{
			Assert.Equal(v.Table.GetName(), x.column.TblName);
			Assert.Equal(v.Table.Foo, x.column.ColName);
			Assert.Equal(nameof(TestEntity.Foo), x.column.ColAlias);
			Assert.Equal(compare, x.compare);
			Assert.IsType<DBNull>(x.value);
		});
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

	[Fact]
	public void Unable_To_Get_Column__Adds_Error__Returns_Original_Query()
	{
		// Arrange
		var (query, v) = Setup();

		// Act
		var result = query.Where(Rnd.Str, Compare.LessThan, Rnd.Guid);

		// Assert
		var fluent = Assert.IsType<FluentQuery<TestEntity, TestId>>(result);
		Assert.Collection(fluent.Errors,
			x => x.AssertType<NoMatchingItemsMsg>()
		);
		Assert.Same(query, fluent);
	}
}
