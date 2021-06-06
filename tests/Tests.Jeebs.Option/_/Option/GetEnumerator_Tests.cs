// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class GetEnumerator_Tests
	{
		[Fact]
		public void When_Some_Returns_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var result = 0;
			foreach (var item in option)
			{
				result = item;
			}

			// Assert
			Assert.Equal(value, result);
		}

		[Fact]
		public void When_None_Does_Nothing()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Create.None<int>();

			// Act
			var result = value;
			foreach (var item in option)
			{
				result = 0;
			}

			// Assert
			Assert.Equal(value, result);
		}
	}
}
