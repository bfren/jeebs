using Xunit;

namespace Jeebs.ArrayExtensions_Tests
{
	public class ExtendWith_Tests
	{
		[Fact]
		public void Array_ReturnsExtendedArray()
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
