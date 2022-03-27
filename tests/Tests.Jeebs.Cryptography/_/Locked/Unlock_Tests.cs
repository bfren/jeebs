// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;
using static Jeebs.Cryptography.Locked.M;

namespace Jeebs.Cryptography.Locked_Tests;

public class Unlock_Tests
{
	[Fact]
	public void No_EncryptedContents_Returns_None_With_UnlockWhenEncryptedContentsIsNullMsg()
	{
		// Arrange
		var box = new Locked<int>();
		var key = CryptoF.GenerateKey().UnsafeUnwrap();

		// Act
		var result = box.Unlock(key);

		// Assert
		result.AssertNone().AssertType<UnlockWhenEncryptedContentsIsNoneMsg>();
	}

	[Fact]
	public void Invalid_Key_Returns_None_With_InvalidKeyExceptionMsg()
	{
		// Arrange
		var value = Rnd.Str;
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key);

		// Act
		var result = box.Unlock(Rnd.ByteF.Get(16));

		// Assert
		result.AssertNone().AssertType<InvalidKeyExceptionMsg>();
	}

	[Fact]
	public void Invalid_Nonce_Returns_None_With_InvalidNonceExceptionMsg()
	{
		// Arrange
		var value = Rnd.Str;
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key)
		{
			Nonce = Rnd.ByteF.Get(8)
		};

		// Act
		var result = box.Unlock(key);

		// Assert
		result.AssertNone().AssertType<InvalidNonceExceptionMsg>();
	}

	[Fact]
	public void Incorrect_Key_Returns_None_With_IncorrectKeyOrNonceMsg()
	{
		// Arrange
		var value = Rnd.Str;
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		var incorrectKey = CryptoF.GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key);

		// Act
		var result = box.Unlock(incorrectKey);

		// Assert
		result.AssertNone().AssertType<IncorrectKeyOrNonceExceptionMsg>();
	}

	[Fact]
	public void Incorrect_Nonce_Returns_None_With_IncorrectKeyOrNonceMsg()
	{
		// Arrange
		var value = Rnd.Str;
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key)
		{
			Nonce = CryptoF.GenerateNonce()
		};

		// Act
		var result = box.Unlock(key);

		// Assert
		result.AssertNone().AssertType<IncorrectKeyOrNonceExceptionMsg>();
	}

	[Fact]
	public void Byte_Key_Returns_Lockable()
	{
		// Arrange
		var value = Rnd.Str;
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key);

		// Act
		var result = box.Unlock(key);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some.Contents);
	}

	[Fact]
	public void String_Key_Returns_Lockable()
	{
		// Arrange
		var value = Rnd.Str;
		var key = Rnd.Str;
		var box = new Locked<string>(value, key);

		// Act
		var result = box.Unlock(key);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some.Contents);
	}
}
