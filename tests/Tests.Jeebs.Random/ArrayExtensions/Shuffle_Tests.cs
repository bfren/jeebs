// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Linq;
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
