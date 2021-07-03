// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;
using static F.DataF.QueryF;

namespace F.DataF.QueryF_Tests
{
	public class GetWhereAndParameters_Tests
	{
		[Fact]
		public void IncludeTableName_False_Calls_Escape_Column()
		{
			// Arrange
			var name = Rnd.Str;
			var column = Substitute.For<IColumn>();
			column.Name.Returns(name);

			var predicates = ImmutableList.Create(new (IColumn, Compare, object)[]
			{
				(column, Compare.LessThan, Rnd.Int)
			});

			var client = Substitute.ForPartsOf<TestClient>();

			// Act
			var (where, param) = GetWhereAndParameters(client, predicates, false);

			// Assert
			Assert.Collection(where,
				x => Assert.Equal($"--{name}-- cmp ~P0", x)
			);
		}

		[Fact]
		public void IncludeTableName_True_Calls_Escape_Column_And_Table()
		{
			// Arrange
			var columnName = Rnd.Str;
			var tableName = Rnd.Str;
			var column = Substitute.For<IColumn>();
			column.Name.Returns(columnName);
			column.Table.Returns(tableName);

			var predicates = ImmutableList.Create(new (IColumn, Compare, object)[]
			{
				(column, Compare.LessThan, Rnd.Int)
			});

			var client = Substitute.ForPartsOf<TestClient>();

			// Act
			var (where, param) = GetWhereAndParameters(client, predicates, true);

			// Assert
			Assert.Collection(where,
				x => Assert.Equal($"--{tableName}|{columnName}-- cmp ~P0", x)
			);
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
			column.Name.Returns(name);

			var value = Rnd.Int;
			var predicates = ImmutableList.Create(new (IColumn, Compare, object)[]
			{
				(column, input, value)
			});

			var client = Substitute.ForPartsOf<TestClient>();

			// Act
			var (where, param) = GetWhereAndParameters(client, predicates, false);

			// Assert
			Assert.Collection(where,
				x => Assert.Equal($"--{name}-- cmp ~P0", x)
			);
			Assert.Collection(param,
				x =>
				{
					Assert.Equal("P0", x.Key);
					Assert.Equal(value, x.Value);
				}
			);
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
			column.Name.Returns(name);

			var value = Rnd.Ulng;
			var predicates = ImmutableList.Create(new (IColumn, Compare, object)[]
			{
				(column, input, new TestId(value))
			});

			var client = Substitute.ForPartsOf<TestClient>();

			// Act
			var (where, param) = GetWhereAndParameters(client, predicates, false);

			// Assert
			Assert.Collection(where,
				x => Assert.Equal($"--{name}-- cmp ~P0", x)
			);
			Assert.Collection(param,
				x =>
				{
					Assert.Equal("P0", x.Key);
					Assert.Equal(value, x.Value);
				}
			);
		}

		private static void Test_In_With_Not_Enumerable(Compare cmp)
		{
			// Arrange
			var column = Substitute.For<IColumn>();
			var predicates = ImmutableList.Create(new (IColumn, Compare, object)[]
			{
				(column, cmp, Rnd.Int)
			});

			var client = Substitute.ForPartsOf<TestClient>();

			// Act
			var (where, param) = GetWhereAndParameters(client, predicates, false);

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

		private static void Test_In_With_Enumerable(Compare cmp, Func<ulong, ulong, ulong, object> getValue)
		{
			// Arrange
			var name = Rnd.Str;
			var column = Substitute.For<IColumn>();
			column.Name.Returns(name);

			var v0 = Rnd.Ulng;
			var v1 = Rnd.Ulng;
			var v2 = Rnd.Ulng;
			var value = getValue(v0, v1, v2);
			var predicates = ImmutableList.Create(new (IColumn, Compare, object)[]
			{
				(column, cmp, value)
			});

			var client = Substitute.ForPartsOf<TestClient>();

			// Act
			var (where, param) = GetWhereAndParameters(client, predicates, false);

			// Assert
			Assert.Collection(where,
				x => Assert.Equal($"--{name}-- cmp (~P0|~P1|~P2)", x)
			);
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
			Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new TestId[] { new(v0), new(v1), new(v2) });
		}

		[Fact]
		public void Operator_Is_In_And_Value_Is_IEnumerable_Joins_Value_And_Adds_Param()
		{
			Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new[] { v0, v1, v2 }.AsEnumerable());
		}

		[Fact]
		public void Operator_Is_In_And_Value_Is_IEnumerable_Joins_Value_And_Adds_StrongId_Param()
		{
			Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new TestId[] { new(v0), new(v1), new(v2) }.AsEnumerable());
		}

		[Fact]
		public void Operator_Is_In_And_Value_Is_List_Joins_Value_And_Adds_Param()
		{
			Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new[] { v0, v1, v2 }.ToList());
		}

		[Fact]
		public void Operator_Is_In_And_Value_Is_List_Joins_Value_And_Adds_StrongId_Param()
		{
			Test_In_With_Enumerable(Compare.In, (v0, v1, v2) => new TestId[] { new(v0), new(v1), new(v2) }.ToList());
		}

		[Fact]
		public void Operator_Is_NotIn_And_Value_Is_Array_Joins_Value_And_Adds_Param()
		{
			Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new[] { v0, v1, v2 });
		}

		[Fact]
		public void Operator_Is_NotIn_And_Value_Is_Array_Joins_Value_And_Adds_StrongId_Param()
		{
			Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new TestId[] { new(v0), new(v1), new(v2) });
		}

		[Fact]
		public void Operator_Is_NotIn_And_Value_Is_IEnumerable_Joins_Value_And_Adds_Param()
		{
			Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new[] { v0, v1, v2 }.AsEnumerable());
		}

		[Fact]
		public void Operator_Is_NotIn_And_Value_Is_IEnumerable_Joins_Value_And_Adds_StrongId_Param()
		{
			Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new TestId[] { new(v0), new(v1), new(v2) }.AsEnumerable());
		}

		[Fact]
		public void Operator_Is_NotIn_And_Value_Is_List_Joins_Value_And_Adds_Param()
		{
			Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new[] { v0, v1, v2 }.ToList());
		}

		[Fact]
		public void Operator_Is_NotIn_And_Value_Is_List_Joins_Value_And_Adds_StrongId_Param()
		{
			Test_In_With_Enumerable(Compare.NotIn, (v0, v1, v2) => new TestId[] { new(v0), new(v1), new(v2) }.ToList());
		}

		public abstract class TestClient : DbClient
		{
			public override string GetOperator(Compare cmp) =>
				"cmp";

			public override string Escape(IColumn column, bool withAlias = false) =>
				$"--{column.Name}--";

			public override string Escape(string column, string table) =>
				$"--{table}|{column}--";

			public override string GetParamRef(string paramName) =>
				$"~{paramName}";

			public override string JoinList(List<string> objects, bool wrap) =>
				$"({string.Join('|', objects)})";
		}

		public sealed record TestId(ulong Value) : StrongId(Value);
	}
}
