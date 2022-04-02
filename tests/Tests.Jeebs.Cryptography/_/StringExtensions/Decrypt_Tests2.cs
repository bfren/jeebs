// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;

namespace Jeebs.Cryptography.StringExtensions_Tests;

public partial class Decrypt_Tests
{
	[Theory]
	[InlineData(null)]
	public void Without_Type_Null_Input_Byte_Key_Returns_Empty(string input)
	{
		// Arrange
		var key = CryptoF.GenerateKey().UnsafeUnwrap();

		// Act
		var result = input.Decrypt(key);

		// Assert
		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Without_Type_Invalid_Json_Input_Byte_Key_Returns_Empty()
	{
		// Arrange
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		var json = Rnd.Str;

		// Act
		var result = json.Decrypt(key);

		// Assert
		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Without_Type_Input_Contents_Invalid_Byte_Key_Returns_Empty()
	{
		// Arrange

		// Act
		var result = defaultInputStringEncryptedWithByteKey.Decrypt(Array.Empty<byte>());

		// Assert
		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Without_Type_Incorrect_Byte_Key_Returns_Empty()
	{
		// Arrange
		var key = CryptoF.GenerateKey().UnsafeUnwrap();

		// Act
		var result = defaultInputStringEncryptedWithByteKey.Decrypt(key);

		// Assert
		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Without_Type_Incorrect_Json_Input_Byte_Key_Returns_Empty()
	{
		// Arrange
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		const string json = /*lang=json,strict*/ "{\"foo\":\"bar\"}";

		// Act
		var result = json.Decrypt(key);

		// Assert
		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Without_Type_Valid_Json_Input_Correct_Byte_Key_Returns_Value()
	{
		// Arrange

		// Act
		var result = defaultInputStringEncryptedWithByteKey.Decrypt(defaultByteKey);

		// Assert
		Assert.Equal(defaultInputString, result);
	}
}
