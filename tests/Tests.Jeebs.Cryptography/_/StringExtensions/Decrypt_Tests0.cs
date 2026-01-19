// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.StringExtensions_Tests;

public partial class Decrypt_Tests
{
	private readonly string defaultInputString = "String to encrypt.";
	private readonly string defaultInputStringEncryptedWithByteKey = /*lang=json,strict*/ "{\"salt\":\"EyWHXBL0TyCMvlLXAd5Ecw==\",\"nonce\":\"KrErRaHfQwWdMif7iuO6bMICMljBkdts\",\"encryptedContents\":\"9PlILzx6y4HqaDhHO9ioqW760nqQ+VXqvhlSNd/u89qqG2DS\"}";
	private readonly byte[] defaultByteKey = Convert.FromBase64String("nXhxz39cHyPx3aZmjeXtNEFTRCzjhVlW+6oVPUPtddA=");

	[Fact]
	public void Null_Input_Byte_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = StringExtensions.Decrypt<int>(null!, key);

		// Assert
		_ = result.AssertFail("Cannot deserialise a null or empty string.");
	}

	[Fact]
	public void Invalid_Json_Input_Byte_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);
		var json = Rnd.Str;

		// Act
		var result = json.Decrypt<int>(key);

		// Assert
		_ = result.AssertFail();
	}

	[Fact]
	public void Input_Contents_Invalid_Byte_Key_Returns_None()
	{
		// Arrange

		// Act
		var result = defaultInputStringEncryptedWithByteKey.Decrypt<int>([]);

		// Assert
		_ = result.AssertFail("Invalid key.");
	}

	[Fact]
	public void Incorrect_Byte_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = defaultInputStringEncryptedWithByteKey.Decrypt<string>(key);

		// Assert
		result.AssertFail("Incorrect key or nonce.");
	}

	[Fact]
	public void Incorrect_Json_Input_Byte_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);
		const string json = /*lang=json,strict*/ "{\"foo\":\"bar\"}";

		// Act
		var result = json.Decrypt<int>(key);

		// Assert
		result.AssertFail("There are no encrypted contents to unlock.");
	}

	[Fact]
	public void Valid_Json_Input_Correct_Byte_Key_Returns_Some()
	{
		// Arrange

		// Act
		var result = defaultInputStringEncryptedWithByteKey.Decrypt<string>(defaultByteKey);

		// Assert
		Assert.Equal(defaultInputString, result);
	}
}
