// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Cryptography.Locked_Tests
{
	public class Unlock_Tests
	{
		[Fact]
		public void No_EncryptedContents_Returns_None_With_UnlockWhenEncryptedContentsIsNullMsg()
		{
			// Arrange
			var box = new Locked<int>();
			var key = F.CryptoF.GenerateKey();

			// Act
			var result = box.Unlock(key);

			// Assert
			var none = Assert.IsType<None<Lockable<int>>>(result);
			Assert.IsType<UnlockWhenEncryptedContentsIsNullMsg>(none.Reason);
		}

		[Fact]
		public void Invalid_Key_Returns_None_With_InvalidKeyExceptionMsg()
		{
			// Arrange
			var value = F.Rnd.Str;
			var key = F.CryptoF.GenerateKey();
			var box = new Locked<string>(value, key);

			// Act
			var result = box.Unlock(F.ByteF.Random(16));

			// Assert
			var none = Assert.IsType<None<Lockable<string>>>(result);
			Assert.IsType<InvalidKeyExceptionMsg>(none.Reason);
		}

		[Fact]
		public void Invalid_Nonce_Returns_None_With_InvalidNonceExceptionMsg()
		{
			// Arrange
			var value = F.Rnd.Str;
			var key = F.CryptoF.GenerateKey();
			var box = new Locked<string>(value, key)
			{
				Nonce = F.ByteF.Random(8)
			};

			// Act
			var result = box.Unlock(key);

			// Assert
			var none = Assert.IsType<None<Lockable<string>>>(result);
			Assert.IsType<InvalidNonceExceptionMsg>(none.Reason);
		}

		[Fact]
		public void Incorrect_Key_Returns_None_With_IncorrectKeyOrNonceMsg()
		{
			// Arrange
			var value = F.Rnd.Str;
			var key = F.CryptoF.GenerateKey();
			var box = new Locked<string>(value, key);

			// Act
			var result = box.Unlock(F.CryptoF.GenerateKey());

			// Assert
			var none = Assert.IsType<None<Lockable<string>>>(result);
			Assert.IsType<IncorrectKeyOrNonceMsg>(none.Reason);
		}

		[Fact]
		public void Incorrect_Nonce_Returns_None_With_IncorrectKeyOrNonceMsg()
		{
			// Arrange
			var value = F.Rnd.Str;
			var key = F.CryptoF.GenerateKey();
			var box = new Locked<string>(value, key)
			{
				Nonce = F.CryptoF.GenerateNonce()
			};

			// Act
			var result = box.Unlock(key);

			// Assert
			var none = Assert.IsType<None<Lockable<string>>>(result);
			Assert.IsType<IncorrectKeyOrNonceMsg>(none.Reason);
		}

		[Fact]
		public void Byte_Key_Returns_Lockable()
		{
			// Arrange
			var value = F.Rnd.Str;
			var key = F.CryptoF.GenerateKey();
			var box = new Locked<string>(value, key);

			// Act
			var result = box.Unlock(key);

			// Assert
			var some = Assert.IsType<Some<Lockable<string>>>(result);
			Assert.Equal(value, some.Value.Contents);
		}

		[Fact]
		public void String_Key_Returns_Lockable()
		{
			// Arrange
			var value = F.Rnd.Str;
			var key = F.Rnd.Str;
			var box = new Locked<string>(value, key);

			// Act
			var result = box.Unlock(key);

			// Assert
			var some = Assert.IsType<Some<Lockable<string>>>(result);
			Assert.Equal(value, some.Value.Contents);
		}
	}
}
