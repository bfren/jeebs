// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.StringExtensions_Tests;

public partial class Decrypt_Tests
{
	[Fact]
	public void Encrypted_String_With_Byte_Key__Decrypts_To_Original()
	{
		// Arrange
		var value = Rnd.Str;
		var key = Rnd.ByteF.Get(32);
		var encrypted = value.Encrypt(key).AssertOk();

		// Act
		var result = encrypted.Decrypt(key);

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void Encrypted_String_With_String_Key__Decrypts_To_Original()
	{
		// Arrange
		var value = Rnd.Str;
		var key = Rnd.Str;
		var encrypted = value.Encrypt(key).AssertOk();

		// Act
		var result = encrypted.Decrypt(key);

		// Assert
		Assert.Equal(value, result);
	}
}
