// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Cryptography;
using Jeebs.Functions;
using Sodium;
using Sodium.Exceptions;

namespace Jeebs.Cryptography.Locked_Tests;

public class Unlock_Tests
{
	[Fact]
	public void Invalid_Key_Returns_Fail_With_KeyOutOfRangeException()
	{
		// Arrange
		var value = Rnd.Str;
		var key = Rnd.ByteF.Get(32);
		var box = new Locked<string>(value, key);

		// Act
		var result = box.Unlock(Rnd.ByteF.Get(16));

		// Assert
		var fail = result.AssertFailure("Invalid key.");
		Assert.IsType<KeyOutOfRangeException>(fail.Exception);
	}

	[Fact]
	public void Invalid_Nonce_Returns_Fail_With_NonceOutOfRangeException()
	{
		// Arrange
		var value = Rnd.Str;
		var key = Rnd.ByteF.Get(32);
		var box = new Locked<string>(value, key)
		{
			Nonce = Rnd.ByteF.Get(8)
		};

		// Act
		var result = box.Unlock(key);

		// Assert
		var fail = result.AssertFailure("Invalid nonce.");
		Assert.IsType<NonceOutOfRangeException>(fail.Exception);
	}

	[Fact]
	public void Incorrect_Key_Returns_Fail_With_CryptographicException()
	{
		// Arrange
		var value = Rnd.Str;
		var key = Rnd.ByteF.Get(32);
		var incorrectKey = Rnd.ByteF.Get(32);
		var box = new Locked<string>(value, key);

		// Act
		var result = box.Unlock(incorrectKey);

		// Assert
		var fail = result.AssertFailure("Incorrect key or nonce.");
		Assert.IsType<CryptographicException>(fail.Exception);
	}

	[Fact]
	public void Incorrect_Nonce_Returns_Fail_With_CryptographicException()
	{
		// Arrange
		var value = Rnd.Str;
		var key = Rnd.ByteF.Get(32);
		var box = new Locked<string>(value, key)
		{
			Nonce = SecretBox.GenerateNonce()
		};

		// Act
		var result = box.Unlock(key);

		// Assert
		var fail = result.AssertFailure("Incorrect key or nonce.");
		Assert.IsType<CryptographicException>(fail.Exception);
	}

	[Fact]
	public void Byte_Key_Returns_Lockable()
	{
		// Arrange
		var value = Rnd.Str;
		var json = JsonF.Serialise(value).Unsafe().Unwrap();
		var key = Rnd.ByteF.Get(32);
		var box = new Locked<string>(json, key);

		// Act
		var result = box.Unlock(key);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(value, ok.Contents);
	}

	[Fact]
	public void String_Key_Returns_Lockable()
	{
		// Arrange
		var value = Rnd.Str;
		var json = JsonF.Serialise(value).Unsafe().Unwrap();
		var key = Rnd.Str;
		var box = new Locked<string>(json, key);

		// Act
		var result = box.Unlock(key);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(value, ok.Contents);
	}
}
