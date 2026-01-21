// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;

namespace Jeebs.Data.Query.Functions.QueryF_Tests;

public class ConvertPredicatesToColumns_Tests
{
	[Fact]
	public void Ignores_Predicate_Property_Not_In_Column_List()
	{
		// Arrange
		var table = new DbName(Rnd.Str);
		var columns = new ColumnList(
		[
			new Column(table, nameof(TestEntity.Id), typeof(TestEntity).GetProperty(nameof(TestEntity.Id))!),
			new Column(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
		]);
		var predicates = new (string column, Compare cmp, dynamic value)[]
		{
			(nameof(TestEntity.Id), Compare.Equal, Rnd.Lng),
			(nameof(TestEntity.Bar), Compare.Equal, Rnd.Int)
		};

		// Act
		var result = QueryF.ConvertPredicatesToColumns(columns, predicates).Unsafe().Unwrap();

		// Assert
		Assert.DoesNotContain(result, x => x.column.ColName == nameof(TestEntity.Bar));
	}

	[Fact]
	public void Converts_Property_To_Name_String_As_Column()
	{
		// Arrange
		var table = new DbName(Rnd.Str);
		var columns = new ColumnList(
		[
			new Column(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
		]);
		var predicates = new (string column, Compare cmp, dynamic value)[]
		{
			(nameof(TestEntity.Foo), Compare.Equal, Rnd.Int)
		};

		// Act
		var result = QueryF.ConvertPredicatesToColumns(columns, predicates).Unsafe().Unwrap();

		// Assert
		var (column, _, _) = Assert.Single(result);
		Assert.Equal(nameof(TestEntity.Foo), column.ColName);
	}

	[Theory]
	[InlineData(Compare.Equal)]
	[InlineData(Compare.In)]
	[InlineData(Compare.LessThan)]
	[InlineData(Compare.LessThanOrEqual)]
	[InlineData(Compare.Like)]
	[InlineData(Compare.MoreThan)]
	[InlineData(Compare.MoreThanOrEqual)]
	[InlineData(Compare.NotEqual)]
	[InlineData(Compare.NotIn)]
	public void Keeps_Original_SearchOperator(Compare input)
	{
		// Arrange
		var table = new DbName(Rnd.Str);
		var columns = new ColumnList(
		[
			new Column(table, nameof(TestEntity.Id), typeof(TestEntity).GetProperty(nameof(TestEntity.Id))!)
		]);
		var predicates = new (string column, Compare cmp, dynamic value)[]
		{
			(nameof(TestEntity.Id), input, Substitute.For<IList>()) // use list type so IN operator doesn't throw exception
		};

		// Act
		var result = QueryF.ConvertPredicatesToColumns(columns, predicates).Unsafe().Unwrap();

		// Assert
		var (_, cmp, _) = Assert.Single(result);
		Assert.Equal(input, cmp);
	}

	[Theory]
	[InlineData(Compare.Equal)]
	[InlineData(Compare.LessThan)]
	[InlineData(Compare.LessThanOrEqual)]
	[InlineData(Compare.Like)]
	[InlineData(Compare.MoreThan)]
	[InlineData(Compare.MoreThanOrEqual)]
	[InlineData(Compare.NotEqual)]
	public void Operator_Not_In_Keeps_Original_Value(Compare input)
	{
		// Arrange
		var table = new DbName(Rnd.Str);
		var columns = new ColumnList(
		[
			new Column(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
		]);
		var value = Rnd.Str;
		var predicates = new (string column, Compare cmp, dynamic value)[]
		{
			(nameof(TestEntity.Foo), input, value)
		};

		// Act
		var result = QueryF.ConvertPredicatesToColumns(columns, predicates).Unsafe().Unwrap();

		// Assert
		var x = Assert.Single(result);
		Assert.Same(value, x.value);
	}

	[Theory]
	[InlineData(Compare.Equal)]
	[InlineData(Compare.LessThan)]
	[InlineData(Compare.LessThanOrEqual)]
	[InlineData(Compare.Like)]
	[InlineData(Compare.MoreThan)]
	[InlineData(Compare.MoreThanOrEqual)]
	[InlineData(Compare.NotEqual)]
	public void Operator_Not_In_Gets_StrongId_Value(Compare input)
	{
		// Arrange
		var table = new DbName(Rnd.Str);
		var columns = new ColumnList(
		[
			new Column(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
		]);
		var id = IdGen.LongId<TestId>();
		var predicates = new (string column, Compare cmp, dynamic value)[]
		{
			(nameof(TestEntity.Foo), input, id)
		};

		// Act
		var result = QueryF.ConvertPredicatesToColumns(columns, predicates).Unsafe().Unwrap();

		// Assert
		var (_, _, value) = Assert.Single(result);
		Assert.Equal(id.Value, value);
	}

	private static void Test_In_With_Enumerable(Func<int, int, int, dynamic> getValue)
	{
		// Arrange
		var table = new DbName(Rnd.Str);
		var columns = new ColumnList(
		[
			new Column(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
		]);

		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var v2 = Rnd.Int;
		var value = getValue(v0, v1, v2);
		var predicates = new (string column, Compare cmp, dynamic value)[]
		{
			(nameof(TestEntity.Foo), Compare.In, value)
		};

		// Act
		var result = QueryF.ConvertPredicatesToColumns(columns, predicates).Unsafe().Unwrap();

		// Assert
		var x = Assert.Single(result);
		Assert.Same(value, x.value);
	}

	[Fact]
	public void Operator_In_With_Array_Keeps_Original_Value()
	{
		Test_In_With_Enumerable((v0, v1, v2) => new[] { v0, v1, v2 });
	}

	[Fact]
	public void Operator_In_With_IEnumerable_Keeps_Original_Value()
	{
		Test_In_With_Enumerable((v0, v1, v2) => new[] { v0, v1, v2 }.AsEnumerable());
	}

	[Fact]
	public void Operator_In_With_List_Keeps_Original_Value()
	{
		Test_In_With_Enumerable((v0, v1, v2) => new[] { v0, v1, v2 }.ToList());
	}

	[Fact]
	public void If_SearchOperator_In_And_Value_Not_IEnumerable_Returns_None_With_InOperatorRequiresValueToBeAListMsg()
	{
		// Arrange
		var table = new DbName(Rnd.Str);
		var columns = new ColumnList(
		[
			new Column(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
		]);
		var value = Rnd.Str;
		var predicates = new (string column, Compare cmp, dynamic value)[]
		{
			(nameof(TestEntity.Foo), Compare.In, value)
		};

		// Act
		var result = QueryF.ConvertPredicatesToColumns(columns, predicates);

		// Assert
		_ = result.AssertFail("IN operator requires value to be a list.");
	}

	public sealed record class TestId : LongId<TestId>;

	public sealed record class TestEntity(string Foo, int Bar) : WithId<TestId, long>;
}
