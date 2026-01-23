// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.Lockable_Tests;

public class Lock_Tests
{
	[Fact]
	public void Empty_Contents_Returns_Fail()
	{
		// Arrange
		var box = new Lockable<string>(null!);
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = box.Lock(key);

		// Assert
		result.AssertFail("Contents cannot be null.");
	}

	[Fact]
	public void Incorrect_Key_Length_Returns_None_With_InvalidKeyLengthMsg()
	{
		// Arrange
		var box = new Lockable<string>(Rnd.Str);
		var key = Rnd.ByteF.Get(20);

		// Act
		var result = box.Lock(key);

		// Assert
		result.AssertFail("Key must be {Bytes} bytes long.", new { Bytes = 32 });
	}

	[Fact]
	public void Byte_Key_Returns_Locked()
	{
		// Arrange
		var box = new Lockable<string>(Rnd.Str);
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = box.Lock(key);

		// Assert
		var ok = result.AssertOk();
		Assert.NotNull(ok.EncryptedContents);
	}

	[Fact]
	public void String_Key_Returns_Locked()
	{
		// Arrange
		var box = new Lockable<string>(Rnd.Str);
		var key = Rnd.Str;

		// Act
		var result = box.Lock(key);

		// Assert
		var ok = result.AssertOk();
		Assert.NotNull(ok.EncryptedContents);
	}
}
