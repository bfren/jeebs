// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static F.OptionF;

namespace Jeebs.OptionExtensions_Tests
{
	public class GetAsyncEnumerator_Tests
	{
		[Fact]
		public async Task When_Some_Returns_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Some(value).AsTask;

			// Act
			var result = 0;
			await foreach (var item in option)
			{
				result = item;
			}

			// Assert
			Assert.Equal(value, result);
		}

		[Fact]
		public async Task When_None_Does_Nothing()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Create.None<int>().AsTask;

			// Act
			var result = value;
			await foreach (var item in option)
			{
				result = 0;
			}

			// Assert
			Assert.Equal(value, result);
		}
	}
}
