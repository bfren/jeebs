using Xunit;

namespace Jeebs.Cryptography.Lockable_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties()
		{
			// Arrange
			var value = F.Rnd.Str;
			var box = new Lockable<string>(value);

			// Act
			var result = box.Contents;

			// Assert
			Assert.Equal(value, result);
		}
	}
}
