// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Jeebs;
using Jeebs.Data;
using Jeebs.Data.Enums;
using NSubstitute;
using Xunit;
using static F.DataF.QueryF;
using static F.DataF.QueryF.Msg;

namespace F.DataF.QueryF_Tests
{
	public class ConvertPredicatesToColumns_Tests
	{
		[Fact]
		public void Ignores_Predicate_Property_Not_In_Column_List()
		{
			// Arrange
			var table = Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Id), typeof(TestEntity).GetProperty(nameof(TestEntity.Id))!) },
				{ new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!) }
			};
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Id, SearchOperator.Equal, Rnd.Lng),
				(e => e.Bar, SearchOperator.Equal, Rnd.Int)
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.DoesNotContain(result, x => x.column.Name == nameof(TestEntity.Bar));
		}

		[Fact]
		public void Converts_Property_To_Name_String_As_Column()
		{
			// Arrange
			var table = Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!) }
			};
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Foo, SearchOperator.Equal, Rnd.Int)
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestEntity.Foo), x.column.Name)
			);
		}

		[Theory]
		[InlineData(SearchOperator.Equal)]
		[InlineData(SearchOperator.In)]
		[InlineData(SearchOperator.LessThan)]
		[InlineData(SearchOperator.LessThanOrEqual)]
		[InlineData(SearchOperator.Like)]
		[InlineData(SearchOperator.MoreThan)]
		[InlineData(SearchOperator.MoreThanOrEqual)]
		[InlineData(SearchOperator.None)]
		[InlineData(SearchOperator.NotEqual)]
		public void Keeps_Original_SearchOperator(SearchOperator input)
		{
			// Arrange
			var table = Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Id), typeof(TestEntity).GetProperty(nameof(TestEntity.Id))!) }
			};
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Id, input, Substitute.For<IList>()) // use list type so IN operator doesn't throw exception
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.Collection(result, x => Assert.Equal(input, x.op));
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
		public void Operator_Not_In_Keeps_Original_Value(SearchOperator input)
		{
			// Arrange
			var table = Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!) }
			};
			var value = Rnd.Str;
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Foo, input, value)
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.Collection(result,
				x => Assert.Same(value, x.value)
			);
		}

		private static void Test_In_With_Enumerable(Func<int, int, int, object> getValue)
		{
			// Arrange
			var table = Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!) }
			};

			var v0 = Rnd.Int;
			var v1 = Rnd.Int;
			var v2 = Rnd.Int;
			var value = getValue(v0, v1, v2);
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Foo, SearchOperator.In, value)
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.Collection(result,
				x => Assert.Same(value, x.value)
			);
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
			var table = Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!) }
			};
			var value = Rnd.Str;
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Foo, SearchOperator.In, value)
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<InOperatorRequiresValueToBeAListMsg>(none);
		}

		public sealed record TestId(long Value) : StrongId(Value);

		public sealed record TestEntity(TestId Id, string Foo, int Bar) : IEntity<TestId>;
	}
}
