using Xunit;

namespace Jeebs.Cryptography.Lockable_Tests
{
	public class Lock_Tests
	{
		[Fact]
		public void Incorrect_Key_Length_Throws_InvalidKeyLengthException()
		{
			// Arrange
			var box = new Lockable<string>(F.Rnd.Str);
			var key = F.ByteF.Random(20);

			// Act
			void action() => box.Lock(key);

			// Assert
			var ex = Assert.Throws<Jx.Cryptography.InvalidKeyLengthException>(action);
			Assert.Equal(string.Format(Jx.Cryptography.InvalidKeyLengthException.Format, Lockable.KeyLength), ex.Message);
		}

		[Fact]
		public void Byte_Key_Returns_Locked()
		{
			// Arrange
			var box = new Lockable<string>(F.Rnd.Str);
			var key = F.ByteF.Random(32);

			// Act
			var result = box.Lock(key);

			// Assert
			Assert.NotNull(result.EncryptedContents);
		}

		[Fact]
		public void String_Key_Returns_Locked()
		{
			// Arrange
			var box = new Lockable<string>(F.Rnd.Str);
			var key = F.Rnd.Str;

			// Act
			var result = box.Lock(key);

			// Assert
			Assert.NotNull(result.EncryptedContents);
		}
	}
}
