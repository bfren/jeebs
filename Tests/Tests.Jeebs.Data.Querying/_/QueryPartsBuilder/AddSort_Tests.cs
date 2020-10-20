using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;
using NSubstitute;
using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.QueryPartsBuilder;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddSort_Tests
	{
		[Fact]
		public void SortRandom_True_Calls_Adapter_GetRandomSortOrder_Sets_Parts_OrderBy()
		{
			// Arrange
			var (builder, adapter) = GetQueryPartsBuilder();
			var sort = F.Rnd.Str;
			adapter.GetRandomSortOrder().Returns(sort);
			var options = new Options { SortRandom = true };

			// Act
			builder.AddSort(options);

			// Assert
			adapter.Received().GetRandomSortOrder();
			Assert.Collection(builder.Parts.OrderBy,
				x => Assert.Equal(sort, x)
			);
		}

		[Fact]
		public void Sets_Parts_OrderBy_With_Specified_Sort_Not_Default_Sort()
		{
			// Arrange
			var (builder, adapter) = GetQueryPartsBuilder();
			adapter.GetSortOrder(Arg.Any<string>(), Arg.Any<SortOrder>())
				.Returns(x => $"{x.ArgAt<string>(0)}:{x.ArgAt<SortOrder>(1)}");

			var c0 = F.Rnd.Str;
			const SortOrder s0 = SortOrder.Ascending;
			var c1 = F.Rnd.Str;
			const SortOrder s1 = SortOrder.Descending;
			var c2 = F.Rnd.Str;
			const SortOrder s2 = SortOrder.Descending;
			var options = new Options { Sort = new[] { (c0, s0), (c1, s1) } };

			// Act
			builder.AddSort(options, (c2, s2));

			// Assert
			adapter.Received().GetSortOrder(c0, s0);
			adapter.Received().GetSortOrder(c1, s1);
			adapter.DidNotReceive().GetSortOrder(c2, s2);
			Assert.Collection(builder.Parts.OrderBy,
				x => Assert.Equal($"{c0}:{s0}", x),
				x => Assert.Equal($"{c1}:{s1}", x)
			);
		}

		[Fact]
		public void Adds_Default_Sort_When_No_Specified_Sort()
		{
			// Arrange
			var (builder, adapter) = GetQueryPartsBuilder();
			adapter.GetSortOrder(Arg.Any<string>(), Arg.Any<SortOrder>())
				.Returns(x => $"{x.ArgAt<string>(0)}:{x.ArgAt<SortOrder>(1)}");

			var c0 = F.Rnd.Str;
			const SortOrder s0 = SortOrder.Ascending;
			var c1 = F.Rnd.Str;
			const SortOrder s1 = SortOrder.Descending;
			var options = new Options();

			// Act
			builder.AddSort(options, (c0, s0), (c1, s1));

			// Assert
			adapter.Received().GetSortOrder(c0, s0);
			adapter.Received().GetSortOrder(c1, s1);
			Assert.Collection(builder.Parts.OrderBy,
				x => Assert.Equal($"{c0}:{s0}", x),
				x => Assert.Equal($"{c1}:{s1}", x)
			);
		}
	}
}
