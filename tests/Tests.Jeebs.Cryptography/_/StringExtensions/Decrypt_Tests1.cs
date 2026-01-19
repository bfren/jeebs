// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.StringExtensions_Tests;

public partial class Decrypt_Tests
{
	private readonly string defaultInputStringEncryptedWithStringKey = /*lang=json,strict*/ "{\"value\":{},\"encryptedContents\":\"RKDdTvdBrxf28cMuuBF+mKkVkYEhJSgwnCnTprGtHeeIMr56\",\"salt\":\"kJ3HSzbuEssDYpGmK9ix1A==\",\"nonce\":\"ehg2foprhsqf7UTrBRpU0cjWvkK0sn/f\"}";
	private readonly string defaultStringKey = "nXhxz39cHyPx3a";

	[Fact]
	public void Null_Input_String_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.Str;

		// Act
		var result = StringExtensions.Decrypt<int>(null!, key);

		// Assert
		_ = result.AssertFail("Cannot deserialise a null or empty string.");
	}

	[Fact]
	public void Invalid_Json_Input_String_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.Str;
		var json = Rnd.Str;

		// Act
		var result = json.Decrypt<int>(key);

		// Assert
		_ = result.AssertFail();
	}

	[Fact]
	public void Input_Contents_Invalid_String_Key_Returns_None()
	{
		// Arrange

		// Act
		var result = defaultInputStringEncryptedWithStringKey.Decrypt<int>(string.Empty);

		// Assert
		result.AssertFail("Incorrect key or nonce.");
	}

	[Fact]
	public void Incorrect_String_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.Str;

		// Act
		var result = defaultInputStringEncryptedWithStringKey.Decrypt<string>(key);

		// Assert
		result.AssertFail("Incorrect key or nonce.");
	}

	[Fact]
	public void Incorrect_Json_Input_String_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.Str;
		const string json = /*lang=json,strict*/ "{\"foo\":\"bar\"}";

		// Act
		var result = json.Decrypt<int>(key);

		// Assert
		result.AssertFail("There are no encrypted contents to unlock.");
	}

	[Fact]
	public void Valid_Json_Input_Correct_String_Key_Returns_Some()
	{
		// Arrange

		// Act
		var result = defaultInputStringEncryptedWithStringKey.Decrypt<string>(defaultStringKey);

		// Assert
		Assert.Equal(defaultInputString, result);
	}

	public class Foo
	{
		public string Bar { get; set; } = string.Empty;
	}
}
