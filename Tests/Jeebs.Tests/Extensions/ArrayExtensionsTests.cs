using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Extensions
{
	public sealed class ArrayExtensionsTests
	{
		[Fact]
		public void ExtendWith_Array_ReturnsExtendedArray()
		{
			// Arrange
			var array = new[] { 1, 2, 3, 4, 5 };
			var expected = new[] { 1, 2, 3, 4, 5,6 };

			// Act
			var result = array.ExtendWith(6);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Shuffle_Array_ReturnsShuffledArray()
		{
			// Arrange
			var array = new[] { 1, 2, 3, 4, 5, 6 };

			// Act
			var result = array.Shuffle();

			// Assert
			Assert.NotEqual(array, result);
		}
	}
}
