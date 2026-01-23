// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.ObjectExtensions_Tests;

public sealed class Lock_Tests
{
	[Fact]
	public void Incorrect_Key_Length_Returns_None_With_InvalidKeyLengthMsg()
	{
		// Arrange
		var key = Rnd.ByteF.Get(20);

		// Act
		var result = Rnd.Guid.Lock(key);

		// Assert
		result.AssertFail("Key must be {Bytes} bytes long.", new { Bytes = 32 });
	}

	[Fact]
	public void Byte_Key_Returns_Locked()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = Rnd.Guid.Lock(key);

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
		var result = Rnd.Guid.Lock(key);

		// Assert
		var ok = result.AssertOk();
		Assert.NotNull(ok.EncryptedContents);
	}
}
