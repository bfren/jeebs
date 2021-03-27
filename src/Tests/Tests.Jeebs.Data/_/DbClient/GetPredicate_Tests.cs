// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbClient_Tests
{
	public class GetPredicate_Tests
	{
		[Fact]
		public void Ignores_Predicate_Property_Not_In_Column_List()
		{
			// Arrange
			var table = F.Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Id), typeof(TestEntity).GetProperty(nameof(TestEntity.Id))!) },
				{ new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!) }
			};
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Id, SearchOperator.Equal, F.Rnd.Lng),
				(e => e.Bar, SearchOperator.Equal, F.Rnd.Int)
			};

			// Act
			var result = DbClient.GetPredicates(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.DoesNotContain(result, x => x.column.Name == nameof(TestEntity.Bar));
		}

		[Fact]
		public void Converts_Property_To_Name_String_As_Column()
		{
			// Arrange
			var table = F.Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!) }
			};
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Foo, SearchOperator.Equal, F.Rnd.Int)
			};

			// Act
			var result = DbClient.GetPredicates(columns, predicates).UnsafeUnwrap();

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
			var table = F.Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Id), typeof(TestEntity).GetProperty(nameof(TestEntity.Id))!) }
			};
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Id, input, Substitute.For<IList>()) // use list type so IN operator doesn't throw exception
			};

			// Act
			var result = DbClient.GetPredicates(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.Collection(result, x => Assert.Equal(input, x.op));
		}

		[Fact]
		public void Keeps_Original_Value()
		{
			// Arrange
			var table = F.Rnd.Str;
			var columns = new MappedColumnList
			{
				{ new MappedColumn(table, nameof(TestEntity.Foo), typeof(TestEntity).GetProperty(nameof(TestEntity.Foo))!) }
			};
			var value = F.Rnd.Str;
			var predicates = new (Expression<Func<TestEntity, object>> column, SearchOperator op, object value)[]
			{
				(e => e.Foo, SearchOperator.Like, value)
			};

			// Act
			var result = DbClient.GetPredicates(columns, predicates).UnsafeUnwrap();

			// Assert
			Assert.Collection(result,
				x => Assert.Same(value, x.value)
			);
		}

		[Fact]
		public void If_SearchOperator_In_And_Value_Not_List_Throws_InvalidQueryPredicateException()
		{
			// Arrange

			// Act

			// Assert
		}

		public sealed record TestId(long Value) : StrongId(Value);

		public sealed record TestEntity(TestId Id, string Foo, int Bar) : IEntity<TestId>;
	}
}
