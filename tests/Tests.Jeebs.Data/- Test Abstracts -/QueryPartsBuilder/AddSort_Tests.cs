// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class AddSort_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
		where TBuilder : QueryPartsBuilder<TId>
		where TId : StrongId
	{
		public abstract void Test00_SortRandom_True_Returns_New_Parts_With_SortRandom_True();

		protected void Test00()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddSort(v.Parts, true, ImmutableList.Empty<(IColumn, SortOrder)>());

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.True(some.SortRandom);
		}

		public abstract void Test01_SortRandom_False_With_Sort_Returns_New_Parts_With_Sort();

		protected void Test01()
		{
			// Arrange
			var c0 = Substitute.For<IColumn>();
			var o0 = SortOrder.Ascending;
			var c1 = Substitute.For<IColumn>();
			var o1 = SortOrder.Descending;
			var sort = ImmutableList.Create((c0, o0), (c1, o1));
			var (builder, v) = Setup();

			// Act
			var result = builder.AddSort(v.Parts, false, sort);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Sort,
				x =>
				{
					Assert.Same(c0, x.column);
					Assert.Equal(o0, x.order);
				},
				x =>
				{
					Assert.Same(c1, x.column);
					Assert.Equal(o1, x.order);
				}
			);
		}

		public abstract void Test02_SortRandom_False_And_Sort_Empty_Returns_Original_Parts();

		protected void Test02()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddSort(v.Parts, false, ImmutableList.Empty<(IColumn, SortOrder)>());

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}
	}
}
