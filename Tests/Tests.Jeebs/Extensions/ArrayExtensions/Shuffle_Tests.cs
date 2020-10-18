using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.ArrayExtensions_Tests
{
	public class Shuffle_Tests
	{
		[Fact]
		public void Array_ReturnsShuffledArray()
		{
			// Arrange
			var array = Enumerable.Range(0, 100).ToArray();

			// Act
			var result = array.Shuffle();

			// Assert
			Assert.NotEqual(array, result);
		}
	}
}
