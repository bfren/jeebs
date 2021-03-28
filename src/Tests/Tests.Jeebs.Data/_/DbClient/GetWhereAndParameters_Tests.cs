// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Data.Enums;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbClient_Tests
{
	public class GetWhereAndParameters_Tests
	{
		[Fact]
		public void IncludeTableName_False_Calls_Escape_Column()
		{
			// Arrange
			var name = F.Rnd.Str;
			var column = Substitute.For<IColumn>();
			column.Name.Returns(name);

			var predicates = new List<(IColumn, SearchOperator, object)>
			{
				(column, SearchOperator.LessThan, F.Rnd.Int)
			};

			var client = Substitute.ForPartsOf<TestClient>();


			// Act
			var (where, param) = client.GetWhereAndParametersTest(predicates, false);

			// Assert
			Assert.Collection(where,
				x => Assert.Equal($"--{name}-- op ~P0", x)
			);
		}

		[Fact]
		public void IncludeTableName_True_Calls_Escape_Column_And_Table()
		{
			// Arrange
			var columnName = F.Rnd.Str;
			var tableName = F.Rnd.Str;
			var column = Substitute.For<IColumn>();
			column.Name.Returns(columnName);
			column.Table.Returns(tableName);

			var predicates = new List<(IColumn, SearchOperator, object)>
			{
				(column, SearchOperator.LessThan, F.Rnd.Int)
			};

			var client = Substitute.ForPartsOf<TestClient>();


			// Act
			var (where, param) = client.GetWhereAndParametersTest(predicates, true);

			// Assert
			Assert.Collection(where,
				x => Assert.Equal($"--{tableName}|{columnName}-- op ~P0", x)
			);
		}

		[Theory]
		[InlineData(SearchOperator.Equal)]
		[InlineData(SearchOperator.LessThan)]
		[InlineData(SearchOperator.LessThanOrEqual)]
		[InlineData(SearchOperator.Like)]
		[InlineData(SearchOperator.MoreThan)]
		[InlineData(SearchOperator.MoreThanOrEqual)]
		[InlineData(SearchOperator.None)]
		[InlineData(SearchOperator.NotEqual)]
		public void Operator_Not_In_Adds_Param(SearchOperator input)
		{
			// Arrange
			var name = F.Rnd.Str;
			var column = Substitute.For<IColumn>();
			column.Name.Returns(name);

			var value = F.Rnd.Int;
			var predicates = new List<(IColumn, SearchOperator, object)>
			{
				(column, input, value)
			};

			var client = Substitute.ForPartsOf<TestClient>();

			// Act
			var (where, param) = client.GetWhereAndParametersTest(predicates, false);

			// Assert
			Assert.Collection(where,
				x => Assert.Equal($"--{name}-- op ~P0", x)
			);
			Assert.Collection(param,
				x =>
				{
					Assert.Equal("P0", x.Key);
					Assert.Equal(value, x.Value);
				}
			);
		}

		[Fact]
		public void Operator_Is_In_And_Value_Not_IList_Ignores_Predicate()
		{
			// Arrange
			var column = Substitute.For<IColumn>();
			var predicates = new List<(IColumn, SearchOperator, object)>
			{
				(column, SearchOperator.In, F.Rnd.Int)
			};

			var client = Substitute.ForPartsOf<TestClient>();

			// Act
			var (where, param) = client.GetWhereAndParametersTest(predicates, false);

			// Assert
			Assert.Empty(where);
			Assert.Empty(param);
		}

		private static void Test_In_With_Enumerable(Func<int, int, int, object> getValue)
		{
			// Arrange
			var name = F.Rnd.Str;
			var column = Substitute.For<IColumn>();
			column.Name.Returns(name);

			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var v2 = F.Rnd.Int;
			var value = getValue(v0, v1, v2);
			var predicates = new List<(IColumn, SearchOperator, object)>
			{
				(column, SearchOperator.In, value)
			};

			var client = Substitute.ForPartsOf<TestClient>();

			// Act
			var (where, param) = client.GetWhereAndParametersTest(predicates, false);

			// Assert
			Assert.Collection(where,
				x => Assert.Equal($"--{name}-- op (~P0|~P1|~P2)", x)
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
			Test_In_With_Enumerable((v0, v1, v2) => new[] { v0, v1, v2 });
		}

		[Fact]
		public void Operator_Is_In_And_Value_Is_IEnumerable_Joins_Value_And_Adds_Param()
		{
			Test_In_With_Enumerable((v0, v1, v2) => new[] { v0, v1, v2 }.AsEnumerable());
		}

		[Fact]
		public void Operator_Is_In_And_Value_Is_List_Joins_Value_And_Adds_Param()
		{
			Test_In_With_Enumerable((v0, v1, v2) => new[] { v0, v1, v2 }.ToList());
		}

		public abstract class TestClient : DbClient
		{
			protected override string GetOperator(SearchOperator op) =>
				"op";

			protected override string Escape(IColumn column) =>
				$"--{column.Name}--";

			protected override string Escape(string column, string table) =>
				$"--{table}|{column}--";

			protected override string GetParamRef(string paramName) =>
				$"~{paramName}";

			protected override string JoinList(List<string> objects, bool wrap) =>
				$"({string.Join('|', objects)})";
		}
	}
}
