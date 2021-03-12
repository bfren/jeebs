// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Cryptography.LockableMsg;
using Xunit;

namespace Jeebs.Cryptography.Lockable_Tests
{
	public class Lock_Tests
	{
		[Fact]
		public void Incorrect_Key_Length_Returns_None_With_InvalidKeyLengthMsg()
		{
			// Arrange
			var box = new Lockable<string>(F.Rnd.Str);
			var key = F.ByteF.Random(20);

			// Act
			var result = box.Lock(key);

			// Assert
			var none = Assert.IsType<None<Locked<string>>>(result);
			Assert.IsType<InvalidKeyLengthMsg>(none.Reason);
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
			var some = Assert.IsType<Some<Locked<string>>>(result);
			Assert.NotNull(some.Value.EncryptedContents);
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
