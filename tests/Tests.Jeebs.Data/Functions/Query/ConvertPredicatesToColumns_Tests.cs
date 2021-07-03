// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Jeebs;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
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
			var columns = new MappedColumnList(new[]
			{
				new MappedColumn(table, nameof(TestEntity.Id), typeof(TestEntity).GetProperty(nameof(TestEntity.Id))!),
				new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
			});
			var predicates = new (Expression<Func<TestEntity, object>> column, Compare cmp, object value)[]
			{
				(e => e.Id, Compare.Equal, Rnd.Lng),
				(e => e.Bar, Compare.Equal, Rnd.Int)
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
			var columns = new MappedColumnList(new[]
			{
				new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
			});
			var predicates = new (Expression<Func<TestEntity, object>> column, Compare cmp, object value)[]
			{
				(e => e.Foo, Compare.Equal, Rnd.Int)
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestEntity.Foo), x.column.Name)
			);
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
			var table = Rnd.Str;
			var columns = new MappedColumnList(new[]
			{
				new MappedColumn(table, nameof(TestEntity.Id), typeof(TestEntity).GetProperty(nameof(TestEntity.Id))!)
			});
			var predicates = new (Expression<Func<TestEntity, object>> column, Compare cmp, object value)[]
			{
				(e => e.Id, input, Substitute.For<IList>()) // use list type so IN operator doesn't throw exception
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.Collection(result, x => Assert.Equal(input, x.cmp));
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
			var table = Rnd.Str;
			var columns = new MappedColumnList(new[]
			{
				new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
			});
			var value = Rnd.Str;
			var predicates = new (Expression<Func<TestEntity, object>> column, Compare cmp, object value)[]
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
			var table = Rnd.Str;
			var columns = new MappedColumnList(new[]
			{
				new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
			});
			var value = Rnd.Ulng;
			var predicates = new (Expression<Func<TestEntity, object>> column, Compare cmp, object value)[]
			{
				(e => e.Foo, input, new TestId(value))
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(value, x.value)
			);
		}

		private static void Test_In_With_Enumerable(Func<int, int, int, object> getValue)
		{
			// Arrange
			var table = Rnd.Str;
			var columns = new MappedColumnList(new[]
			{
				new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
			});

			var v0 = Rnd.Int;
			var v1 = Rnd.Int;
			var v2 = Rnd.Int;
			var value = getValue(v0, v1, v2);
			var predicates = new (Expression<Func<TestEntity, object>> column, Compare cmp, object value)[]
			{
				(e => e.Foo, Compare.In, value)
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
			var columns = new MappedColumnList(new[]
			{
				new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!)
			});
			var value = Rnd.Str;
			var predicates = new (Expression<Func<TestEntity, object>> column, Compare cmp, object value)[]
			{
				(e => e.Foo, Compare.In, value)
			};

			// Act
			var result = ConvertPredicatesToColumns(columns, predicates);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<InOperatorRequiresValueToBeAListMsg>(none);
		}

		public sealed record TestId(ulong Value) : StrongId(Value);

		public sealed record TestEntity(TestId Id, string Foo, int Bar) : IWithId<TestId>;
	}
}
