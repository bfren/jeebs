// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Cryptography.Lockable.M;

namespace Jeebs.Cryptography.Functions.CryptoF_Tests;

public sealed class Lock_Tests
{
	[Fact]
	public void Incorrect_Key_Length_Returns_None_With_InvalidKeyLengthMsg()
	{
		// Arrange
		var key = Rnd.ByteF.Get(20);

		// Act
		var result = CryptoF.Lock(Rnd.Str, key);

		// Assert
		result.AssertNone().AssertType<InvalidKeyLengthMsg>();
	}

	[Fact]
	public void Byte_Key_Returns_Locked()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = CryptoF.Lock(Rnd.Str, key);

		// Assert
		var some = result.AssertSome();
		Assert.NotNull(some.EncryptedContents);
	}

	[Fact]
	public void String_Key_Returns_Locked()
	{
		// Arrange
		var key = Rnd.Str;

		// Act
		var result = CryptoF.Lock(Rnd.Str, key);

		// Assert
		Assert.NotNull(result.EncryptedContents);
	}
}
