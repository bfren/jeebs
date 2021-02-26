using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.OptionExtensions_Tests
{
	public class Where_Tests
	{
		[Fact]
		public void Linq_Where_True_With_Some_Returns_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);

			// Act
			var result = from a in option
						 where a == value
						 select a ^ 2;

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value ^ 2, some.Value);
		}

		[Fact]
		public void Linq_Where_False_With_Some_Returns_None()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);

			// Act
			var result = from a in option
						 where a != value
						 select a ^ 2;

			// Assert
			Assert.IsType<None<int>>(result);
		}

		[Fact]
		public void Linq_Where_With_None_Returns_None()
		{
			// Arrange
			var option = Option.None<int>().AddReason<InvalidIntegerMsg>();

			// Act
			var result = from a in option
						 where a == 0
						 select a ^ 2;

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.True(none.Reason is InvalidIntegerMsg);
		}

		public class InvalidIntegerMsg : IMsg { }
	}
}
