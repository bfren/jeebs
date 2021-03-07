// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
