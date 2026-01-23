// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.Functions.CryptoF_Tests;

public sealed class Lock_Tests
{
	[Fact]
	public void Empty_Contents_Returns_Fail()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = CryptoF.Lock<string>(null!, key);

		// Assert
		result.AssertFail("Contents cannot be null.");
	}

	[Fact]
	public void Incorrect_Key_Length_Returns_Fail()
	{
		// Arrange
		var key = Rnd.ByteF.Get(20);

		// Act
		var result = CryptoF.Lock(Rnd.Str, key);

		// Assert
		result.AssertFail("Key must be {Bytes} bytes long.", 32);
	}

	[Fact]
	public void Byte_Key_Returns_Locked()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = CryptoF.Lock(Rnd.Str, key);

		// Assert
		var ok = result.AssertOk();
		Assert.NotNull(ok.EncryptedContents);
	}

	[Fact]
	public void String_Key_Returns_Locked()
	{
		// Arrange
		var key = Rnd.Str;

		// Act
		var result = CryptoF.Lock(Rnd.Str, key);

		// Assert
		var ok = result.AssertOk();
		Assert.NotNull(ok.EncryptedContents);
	}
}
