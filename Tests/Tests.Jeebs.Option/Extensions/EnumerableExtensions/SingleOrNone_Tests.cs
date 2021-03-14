// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Linq;
using NSubstitute;
using Xunit;
using static F.OptionF.Enumerable.Msg;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class SingleOrNone_Tests
	{
		[Fact]
		public void Empty_List_Returns_None_With_ListIsEmptyMsg()
		{
			// Arrange
			var list = Array.Empty<int>();

			// Act
			var result = list.SingleOrNone();

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<ListIsEmptyMsg>(none.Reason);
		}

		[Fact]
		public void Multiple_Items_Returns_None_With_MultipleItemsMsg()
		{
			// Arrange
			var list = new int?[] { F.Rnd.Int, F.Rnd.Int, F.Rnd.Int };
			var predicate = Substitute.For<Func<int?, bool>>();

			// Act
			var result = list.SingleOrNone();

			// Assert
			var none = Assert.IsType<None<int?>>(result);
			Assert.IsType<MultipleItemsMsg>(none.Reason);
		}

		[Fact]
		public void No_Matching_Items_Returns_None_With_NoMatchingItemsMsg()
		{
			// Arrange
			var list = new int?[] { F.Rnd.Int, F.Rnd.Int, F.Rnd.Int };
			var predicate = Substitute.For<Func<int?, bool>>();
			predicate.Invoke(Arg.Any<int?>()).Returns(false);

			// Act
			var result = list.SingleOrNone(predicate);

			// Assert
			var none = Assert.IsType<None<int?>>(result);
			Assert.IsType<NoMatchingItemsMsg>(none.Reason);
		}

		[Fact]
		public void Null_Item_Returns_None_With_NullItemMsg()
		{
			// Arrange
			var list = new int?[] { F.Rnd.Int, null, F.Rnd.Int };
			var predicate = Substitute.For<Func<int?, bool>>();
			predicate.Invoke(null).Returns(true);

			// Act
			var result = list.SingleOrNone(predicate);

			// Assert
			var none = Assert.IsType<None<int?>>(result);
			Assert.IsType<NullItemMsg>(none.Reason);
		}

		[Fact]
		public void Returns_Single_Element()
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new[] { value };

			// Act
			var result = list.LastOrNone();

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public void Returns_Single_Matching_Element()
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new[] { F.Rnd.Int, value, F.Rnd.Int };
			var predicate = Substitute.For<Func<int, bool>>();
			predicate.Invoke(value).Returns(true);

			// Act
			var result = list.LastOrNone(predicate);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}
	}
}
