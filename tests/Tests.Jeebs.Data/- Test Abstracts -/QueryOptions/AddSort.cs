// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public abstract class AddSort<TOptions, TId> : QueryOptions_Tests<TOptions, TId>
		where TOptions : QueryOptions<TId>
		where TId : StrongId
	{
		public abstract void Test00_SortRandom_True_Returns_New_Parts_With_SortRandom_True();

		protected void Test00()
		{
			// Arrange
			var (options, v) = Setup(opt => opt with { SortRandom = true });

			// Act
			var result = options.AddSortTest(v.Parts);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.True(some.SortRandom);
		}

		public abstract void Test01_SortRandom_True_And_Sort_Not_Null_Returns_New_Parts_With_SortRandom_True();

		protected void Test01()
		{
			// Arrange
			var sort = Array.Empty<(IColumn, SortOrder)>();
			var (options, v) = Setup(opt => opt with { SortRandom = true });

			// Act
			var result = options.AddSortTest(v.Parts);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.True(some.SortRandom);
		}

		public abstract void Test02_SortRandom_False_And_Sort_Not_Null_Returns_New_Parts_With_Sort();

		protected void Test02()
		{
			// Arrange
			var c0 = Substitute.For<IColumn>();
			var o0 = SortOrder.Ascending;
			var c1 = Substitute.For<IColumn>();
			var o1 = SortOrder.Descending;
			var sort = new[] { (c0, o0), (c1, o1) };
			var (options, v) = Setup(opt => opt with { Sort = sort });

			// Act
			var result = options.AddSortTest(v.Parts);

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

		public abstract void Test03_SortRandom_False_And_Sort_Null_Returns_Original_Parts();

		protected void Test03()
		{
			// Arrange
			var (options, v) = Setup();

			// Act
			var result = options.AddSortTest(v.Parts);

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}
	}
}
