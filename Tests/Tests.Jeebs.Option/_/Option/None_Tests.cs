using Xunit;

namespace Jeebs.Option_Tests
{
	public class None_Tests
	{
		[Fact]
		public void Returns_None()
		{
			// Arrange

			// Act
			var result = Option.None<int>();

			// Assert
			Assert.IsType<None<int>>(result);
		}
	}
}
