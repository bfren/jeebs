// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.CryptoF;
using static Jeebs.Cryptography.Locked.Msg;

namespace Jeebs.Cryptography.Locked_Tests;

public class Unlock_Tests
{
	[Fact]
	public void No_EncryptedContents_Returns_None_With_UnlockWhenEncryptedContentsIsNullMsg()
	{
		// Arrange
		var box = new Locked<int>();
		var key = GenerateKey().UnsafeUnwrap();

		// Act
		var result = box.Unlock(key);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnlockWhenEncryptedContentsIsNoneMsg>(none);
	}

	[Fact]
	public void Invalid_Key_Returns_None_With_InvalidKeyExceptionMsg()
	{
		// Arrange
		var value = F.Rnd.Str;
		var key = GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key);

		// Act
		var result = box.Unlock(F.Rnd.ByteF.Get(16));

		// Assert
		var none = result.AssertNone();
		Assert.IsType<InvalidKeyExceptionMsg>(none);
	}

	[Fact]
	public void Invalid_Nonce_Returns_None_With_InvalidNonceExceptionMsg()
	{
		// Arrange
		var value = F.Rnd.Str;
		var key = GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key)
		{
			Nonce = F.Rnd.ByteF.Get(8)
		};

		// Act
		var result = box.Unlock(key);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<InvalidNonceExceptionMsg>(none);
	}

	[Fact]
	public void Incorrect_Key_Returns_None_With_IncorrectKeyOrNonceMsg()
	{
		// Arrange
		var value = F.Rnd.Str;
		var key = GenerateKey().UnsafeUnwrap();
		var incorrectKey = GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key);

		// Act
		var result = box.Unlock(incorrectKey);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<IncorrectKeyOrNonceExceptionMsg>(none);
	}

	[Fact]
	public void Incorrect_Nonce_Returns_None_With_IncorrectKeyOrNonceMsg()
	{
		// Arrange
		var value = F.Rnd.Str;
		var key = GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key)
		{
			Nonce = GenerateNonce()
		};

		// Act
		var result = box.Unlock(key);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<IncorrectKeyOrNonceExceptionMsg>(none);
	}

	[Fact]
	public void Byte_Key_Returns_Lockable()
	{
		// Arrange
		var value = F.Rnd.Str;
		var key = GenerateKey().UnsafeUnwrap();
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
		var value = F.Rnd.Str;
		var key = F.Rnd.Str;
		var box = new Locked<string>(value, key);

		// Act
		var result = box.Unlock(key);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some.Contents);
	}
}
