// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF.Enumerable.Msg;

namespace Jeebs_Tests.Enumerable
{
	public abstract class LastOrNone_Tests
	{
		public abstract void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg();

		protected static void Test00(Func<IEnumerable<int>, Option<int>> act)
		{
			// Arrange
			var list = Array.Empty<int>();

			// Act
			var result = act(list);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<ListIsEmptyMsg>(none);
		}

		public abstract void Test01_No_Matching_Items_Returns_None_With_LastItemIsNullMsg();

		protected static void Test01(Func<IEnumerable<int?>, Func<int?, bool>, Option<int?>> act)
		{
			// Arrange
			var list = new int?[] { F.Rnd.Int, F.Rnd.Int, F.Rnd.Int };
			var predicate = Substitute.For<Func<int?, bool>>();
			predicate.Invoke(Arg.Any<int?>()).Returns(false);

			// Act
			var result = act(list, predicate);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<LastItemIsNullMsg>(none);
		}

		public abstract void Test02_Returns_Last_Element();

		protected static void Test02(Func<IEnumerable<int>, Option<int>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new[] { F.Rnd.Int, F.Rnd.Int, value };

			// Act
			var result = act(list);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}

		public abstract void Test03_Returns_Last_Matching_Element();

		protected static void Test03(Func<IEnumerable<int>, Func<int, bool>, Option<int>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new[] { F.Rnd.Int, value, F.Rnd.Int };
			var predicate = Substitute.For<Func<int, bool>>();
			predicate.Invoke(value).Returns(true);

			// Act
			var result = act(list, predicate);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}
	}
}
