// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Functions;

namespace Jeebs.Data.Common.Functions.QueryF_Tests;

public class GetWhereAndParameters_Tests
{
	[Fact]
	public void IncludeTableName_False_Calls_Escape_Column()
	{
		// Arrange
		var name = Rnd.Str;
		var column = Substitute.For<IColumn>();
		column.ColName.Returns(name);

		var predicates = ListF.Create(
		[
			(column, Compare.LessThan, (dynamic)Rnd.Int)
		]);

		var adapter = Substitute.For<IAdapter>();
		var client = Substitute.ForPartsOf<TestClient>(adapter);

		// Act
		var (where, param) = QueryF.GetWhereAndParameters(client, predicates, false);

		// Assert
		var single = Assert.Single(where);
		Assert.Equal($"--{name}-- cmp ~P0", single);
	}

	[Fact]
	public void IncludeTableName_True_Calls_Escape_Column_And_Table()
	{
		// Arrange
		var columnName = Rnd.Str;
		var tableName = new TableName(Rnd.Str);
		var column = Substitute.For<IColumn>();
		column.ColName.Returns(columnName);
		column.TblName.Returns(tableName);

		var predicates = ListF.Create(
		[
			(column, Compare.LessThan, (dynamic)Rnd.Int)
		]);

		var adapter = Substitute.For<IAdapter>();
		var client = Substitute.ForPartsOf<TestClient>(adapter);

		// Act
		var (where, param) = QueryF.GetWhereAndParameters(client, predicates, true);

		// Assert
		var x = // Assert
Assert.Single(where);
		Assert.Equal($"--{tableName}|{columnName}-- cmp ~P0", x);
	}

	[Theory]
	[InlineData(Compare.Equal)]
	[InlineData(Compare.LessThan)]
	[InlineData(Compare.LessThanOrEqual)]
	[InlineData(Compare.Like)]
	[InlineData(Compare.MoreThan)]
	[InlineData(Compare.MoreThanOrEqual)]
	[InlineData(Compare.NotEqual)]
	public void Operator_Not_In_Adds_Param(Compare input)
	{
		// Arrange
		var name = Rnd.Str;
		var column = Substitute.For<IColumn>();
		column.ColName.Returns(name);

		var value = Rnd.Int;
		var predicates = ListF.Create(
		[
			(column, input, (dynamic)value)
		]);

		var adapter = Substitute.For<IAdapter>();
		var client = Substitute.ForPartsOf<TestClient>(adapter);

		// Act
		var (where, param) = QueryF.GetWhereAndParameters(client, predicates, false);

		// Assert
		var x = Assert.Single(where);
		Assert.Equal($"--{name}-- cmp ~P0", x);
		var single = Assert.Single(param);
		Assert.Equal("P0", single.Key);
		Assert.Equal(value, single.Value);
	}

	[Theory]
	[InlineData(Compare.Equal)]
	[InlineData(Compare.LessThan)]
	[InlineData(Compare.LessThanOrEqual)]
	[InlineData(Compare.Like)]
	[InlineData(Compare.MoreThan)]
	[InlineData(Compare.MoreThanOrEqual)]
	[InlineData(Compare.NotEqual)]
	public void Operator_Not_In_Adds_StrongId_Param(Compare input)
	{
		// Arrange
		var name = Rnd.Str;
		var column = Substitute.For<IColumn>();
		column.ColName.Returns(name);

		var value = Rnd.Lng;
		var predicates = ListF.Create(
		[
			(column, input, (dynamic)TestId.Wrap(value))
		]);

		var adapter = Substitute.For<IAdapter>();
		var client = Substitute.ForPartsOf<TestClient>(adapter);

		// Act
		var (where, param) = QueryF.GetWhereAndParameters(client, predicates, false);

		// Assert
		var x = Assert.Single(where);
		Assert.Equal($"--{name}-- cmp ~P0", x);
		var single = Assert.Single(param);
		Assert.Equal("P0", single.Key);
		Assert.Equal(value, single.Value);
	}

	private static void Test_In_With_Not_Enumerable(Compare cmp)
	{
		// Arrange
		var column = Substitute.For<IColumn>();
		var predicates = ListF.Create(
		[
			(column, cmp, (dynamic)Rnd.Int)
		]);

		var adapter = Substitute.For<IAdapter>();
		var client = Substitute.ForPartsOf<TestClient>(adapter);

		// Act
		var (where, param) = QueryF.GetWhereAndParameters(client, predicates, false);

		// Assert
		Assert.Empty(where);
		Assert.Empty(param);
	}

	[Fact]
	public void Operator_Is_In_And_Value_Not_Enumerable_Ignores_Predicate()
	{
		Test_In_With_Not_Enumerable(Compare.In);
	}

	[Fact]
	public void Operator_Is_NotIn_And_Value_Not_Enumerable_Ignores_Predicate()
	{
		Test_In_With_Not_Enumerable(Compare.NotIn);
	}

	private static void Test_In_With_Enumerable(Compare cmp, Func<long, long, long, object> getValue)
	{
		// Arrange
		var name = Rnd.Str;
		var column = Substitute.For<IColumn>();
		column.ColName.Returns(name);

		var v0 = Rnd.Lng;
		var v1 = Rnd.Lng;
		var v2 = Rnd.Lng;
		var value = getValue(v0, v1, v2);
		var predicates = ListF.Create(
		[
			(column, cmp, value)
		]);

		var adapter = Substitute.For<IAdapter>();
		var client = Substitute.ForPartsOf<TestClient>(adapter);

		// Act
		var (where, param) = QueryF.GetWhereAndParameters(client, predicates, false);

		// Assert
		var x = // Assert
Assert.Single(where);
		Assert.Equal($"--{name}-- cmp (~P0|~P1|~P2)", x);
		Assert.Collection(param,
			x =>
			{
				Assert.Equal("P0", x.Key);
				Assert.Equal(v0, x.Value);
			},
			x =>
			{
				Assert.Equal("P1", x.Key);
				Assert.Equal(v1, x.Value);
			},
			x =>
			{
				Assert.Equal("P2", x.Key);
				Assert.Equal(v2, x.Value);
			}
		);
	}

	[Fact]
	public void Operator_Is_In_And_Value_Is_Array_Joins_Value_And_Adds_Param()
	{
		Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new[] { v0, v1, v2 });
	}

	[Fact]
	public void Operator_Is_In_And_Value_Is_Array_Joins_Value_And_Adds_StrongId_Param()
	{
		Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new[] { TestId.Wrap(v0), TestId.Wrap(v1), TestId.Wrap(v2) });
	}

	[Fact]
	public void Operator_Is_In_And_Value_Is_IEnumerable_Joins_Value_And_Adds_Param()
	{
		Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new[] { v0, v1, v2 }.AsEnumerable());
	}

	[Fact]
	public void Operator_Is_In_And_Value_Is_IEnumerable_Joins_Value_And_Adds_StrongId_Param()
	{
		Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new[] { TestId.Wrap(v0), TestId.Wrap(v1), TestId.Wrap(v2) }.AsEnumerable());
	}

	[Fact]
	public void Operator_Is_In_And_Value_Is_List_Joins_Value_And_Adds_Param()
	{
		Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new[] { v0, v1, v2 }.ToList());
	}

	[Fact]
	public void Operator_Is_In_And_Value_Is_List_Joins_Value_And_Adds_StrongId_Param()
	{
		Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new[] { TestId.Wrap(v0), TestId.Wrap(v1), TestId.Wrap(v2) }.ToList());
	}

	[Fact]
	public void Operator_Is_NotIn_And_Value_Is_Array_Joins_Value_And_Adds_Param()
	{
		Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new[] { v0, v1, v2 });
	}

	[Fact]
	public void Operator_Is_NotIn_And_Value_Is_Array_Joins_Value_And_Adds_StrongId_Param()
	{
		Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new[] { TestId.Wrap(v0), TestId.Wrap(v1), TestId.Wrap(v2) });
	}

	[Fact]
	public void Operator_Is_NotIn_And_Value_Is_IEnumerable_Joins_Value_And_Adds_Param()
	{
		Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new[] { v0, v1, v2 }.AsEnumerable());
	}

	[Fact]
	public void Operator_Is_NotIn_And_Value_Is_IEnumerable_Joins_Value_And_Adds_StrongId_Param()
	{
		Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new[] { TestId.Wrap(v0), TestId.Wrap(v1), TestId.Wrap(v2) }.AsEnumerable());
	}

	[Fact]
	public void Operator_Is_NotIn_And_Value_Is_List_Joins_Value_And_Adds_Param()
	{
		Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new[] { v0, v1, v2 }.ToList());
	}

	[Fact]
	public void Operator_Is_NotIn_And_Value_Is_List_Joins_Value_And_Adds_StrongId_Param()
	{
		Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new[] { TestId.Wrap(v0), TestId.Wrap(v1), TestId.Wrap(v2) }.ToList());
	}

	public abstract class TestClient(IAdapter adapter) : DbClient(adapter)
	{
		public override string GetOperator(Compare cmp) =>
			"cmp";

		public override string Escape(IColumn column) =>
			Escape(column, false);

		public override string Escape(IColumn column, bool withAlias) =>
			$"--{column.ColName}--";

		public override string Escape(ITableName table, string column) =>
			$"--{table}|{column}--";

		public override string GetParamRef(string paramName) =>
			$"~{paramName}";

		public override string JoinList(List<string> objects, bool wrap) =>
			$"({string.Join('|', objects)})";
	}

	private sealed record class TestId : LongId<TestId>;
}
