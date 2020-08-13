using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class ArrayExtensions_Tests
	{
		[Fact]
		public void ExtendWith_Array_ReturnsExtendedArray()
		{
			// Arrange
			var array = new[] { 1, 2, 3, 4, 5 };
			var expected = new[] { 1, 2, 3, 4, 5, 6 };

			// Act
			var result = array.ExtendWith(6);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
