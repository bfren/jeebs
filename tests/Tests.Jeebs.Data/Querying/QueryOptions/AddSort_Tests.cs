// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddSort_Tests
	{
		[Fact]
		public void SortRandom_True_Returns_New_Parts_With_SortRandom_True()
		{
			// Arrange
			var (_, _, parts, options) = QueryOptions_Setup.Get(opt => opt with { SortRandom = true });

			// Act
			var result = options.AddSortTest(parts);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(parts, some);
			Assert.True(some.SortRandom);
		}

		[Fact]
		public void SortRandom_True_And_Sort_Not_Null_Returns_New_Parts_With_SortRandom_True()
		{
			// Arrange
			var sort = Array.Empty<(IColumn, SortOrder)>();
			var (_, _, parts, options) = QueryOptions_Setup.Get(opt => opt with { Sort = sort, SortRandom = true });

			// Act
			var result = options.AddSortTest(parts);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(parts, some);
			Assert.True(some.SortRandom);
		}

		[Fact]
		public void SortRandom_False_And_Sort_Not_Null_Returns_New_Parts_With_Sort()
		{
			// Arrange
			var c0 = Substitute.For<IColumn>();
			var o0 = SortOrder.Ascending;
			var c1 = Substitute.For<IColumn>();
			var o1 = SortOrder.Descending;
			var sort = new[] { (c0, o0), (c1, o1) };
			var (_, _, parts, options) = QueryOptions_Setup.Get(opt => opt with { Sort = sort });

			// Act
			var result = options.AddSortTest(parts);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(parts, some);
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
		public void SortRandom_False_And_Sort_Null_Returns_Original_Parts()
		{
			// Arrange
			var (_, _, parts, options) = QueryOptions_Setup.Get();

			// Act
			var result = options.AddSortTest(parts);

			// Assert
			var some = result.AssertSome();
			Assert.Same(parts, some);
		}
	}
}
