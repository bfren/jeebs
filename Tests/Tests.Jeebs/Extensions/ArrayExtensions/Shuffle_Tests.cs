using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class ArrayExtensions_Tests
	{
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
