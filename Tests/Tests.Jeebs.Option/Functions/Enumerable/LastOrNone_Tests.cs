﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public class LastOrNone_Tests
	{
		[Fact]
		public void Empty_List_Returns_None_With_ListIsEmptyMsg()
		{
			// Arrange
			var list = Array.Empty<int>();

			// Act
			var result = LastOrNone(list, null);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<ListIsEmptyMsg>(none.Reason);
		}

		[Fact]
		public void No_Matching_Items_Returns_None_With_LastItemIsNullMsg()
		{
			// Arrange
			var list = new int?[] { Rnd.Int, Rnd.Int, Rnd.Int };
			var predicate = Substitute.For<Func<int?, bool>>();
			predicate.Invoke(Arg.Any<int?>()).Returns(false);

			// Act
			var result = LastOrNone(list, predicate);

			// Assert
			var none = Assert.IsType<None<int?>>(result);
			Assert.IsType<LastItemIsNullMsg>(none.Reason);
		}

		[Fact]
		public void Returns_Last_Element()
		{
			// Arrange
			var value = Rnd.Int;
			var list = new[] { Rnd.Int, Rnd.Int, value };

			// Act
			var result = LastOrNone(list, null);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public void Returns_Last_Matching_Element()
		{
			// Arrange
			var value = Rnd.Int;
			var list = new[] { Rnd.Int, value, Rnd.Int };
			var predicate = Substitute.For<Func<int, bool>>();
			predicate.Invoke(value).Returns(true);

			// Act
			var result = LastOrNone(list, predicate);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}
	}
}