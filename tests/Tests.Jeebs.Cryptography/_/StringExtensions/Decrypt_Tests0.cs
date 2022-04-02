// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;
using static Jeebs.Cryptography.Locked.M;
using static Jeebs.Functions.JsonF.M;

namespace Jeebs.Cryptography.StringExtensions_Tests;

public partial class Decrypt_Tests
{
	private readonly string defaultInputString = "String to encrypt.";
	private readonly string defaultInputStringEncryptedWithByteKey = /*lang=json,strict*/ "{\"salt\":\"EyWHXBL0TyCMvlLXAd5Ecw==\",\"nonce\":\"KrErRaHfQwWdMif7iuO6bMICMljBkdts\",\"encryptedContents\":\"9PlILzx6y4HqaDhHO9ioqW760nqQ+VXqvhlSNd/u89qqG2DS\"}";
	private readonly byte[] defaultByteKey = Convert.FromBase64String("nXhxz39cHyPx3aZmjeXtNEFTRCzjhVlW+6oVPUPtddA=");

	[Theory]
	[InlineData(null)]
	public void Null_Input_Byte_Key_Returns_None(string input)
	{
		// Arrange
		var key = CryptoF.GenerateKey().UnsafeUnwrap();

		// Act
		var result = input.Decrypt<int>(key);

		// Assert
		result.AssertNone().AssertType<DeserialisingNullOrEmptyStringMsg>();
	}

	[Fact]
	public void Invalid_Json_Input_Byte_Key_Returns_None()
	{
		// Arrange
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		var json = Rnd.Str;

		// Act
		var result = json.Decrypt<int>(key);

		// Assert
		result.AssertNone().AssertType<DeserialiseExceptionMsg>();
	}

	[Fact]
	public void Input_Contents_Invalid_Byte_Key_Returns_None()
	{
		// Arrange

		// Act
		var result = defaultInputStringEncryptedWithByteKey.Decrypt<int>(Array.Empty<byte>());

		// Assert
		result.AssertNone().AssertType<InvalidKeyExceptionMsg>();
	}

	[Fact]
	public void Incorrect_Byte_Key_Returns_None()
	{
		// Arrange
		var key = CryptoF.GenerateKey().UnsafeUnwrap();

		// Act
		var result = defaultInputStringEncryptedWithByteKey.Decrypt<string>(key);

		// Assert
		result.AssertNone().AssertType<IncorrectKeyOrNonceExceptionMsg>();
	}

	[Fact]
	public void Incorrect_Json_Input_Byte_Key_Returns_None()
	{
		// Arrange
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		const string json = /*lang=json,strict*/ "{\"foo\":\"bar\"}";

		// Act
		var result = json.Decrypt<int>(key);

		// Assert
		result.AssertNone().AssertType<UnlockWhenEncryptedContentsIsNoneMsg>();
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
