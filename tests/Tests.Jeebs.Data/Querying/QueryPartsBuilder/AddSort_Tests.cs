// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddSort_Tests : QueryPartsBuilder_Tests
	{
		[Fact]
		public void SortRandom_True_Returns_New_Parts_With_SortRandom_True()
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

		[Fact]
		public void SortRandom_False_With_Sort_Returns_New_Parts_With_Sort()
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

		[Fact]
		public void SortRandom_False_And_Sort_Empty_Returns_Original_Parts()
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
