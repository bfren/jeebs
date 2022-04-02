// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Cryptography.Locked.M;
using static Jeebs.Functions.JsonF.M;

namespace Jeebs.Cryptography.StringExtensions_Tests;

public partial class Decrypt_Tests
{
	private readonly string defaultInputStringEncryptedWithStringKey = /*lang=json,strict*/ "{\"value\":{},\"encryptedContents\":\"RKDdTvdBrxf28cMuuBF+mKkVkYEhJSgwnCnTprGtHeeIMr56\",\"salt\":\"kJ3HSzbuEssDYpGmK9ix1A==\",\"nonce\":\"ehg2foprhsqf7UTrBRpU0cjWvkK0sn/f\"}";
	private readonly string defaultStringKey = "nXhxz39cHyPx3a";

	[Theory]
	[InlineData(null)]
	public void Null_Input_String_Key_Returns_None(string input)
	{
		// Arrange
		var key = Rnd.Str;

		// Act
		var result = input.Decrypt<int>(key);

		// Assert
		result.AssertNone().AssertType<DeserialisingNullOrEmptyStringMsg>();
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
		result.AssertNone().AssertType<DeserialiseExceptionMsg>();
	}

	[Fact]
	public void Input_Contents_Invalid_String_Key_Returns_None()
	{
		// Arrange

		// Act
		var result = defaultInputStringEncryptedWithStringKey.Decrypt<int>(string.Empty);

		// Assert
		result.AssertNone().AssertType<IncorrectKeyOrNonceExceptionMsg>();
	}

	[Fact]
	public void Incorrect_String_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.Str;

		// Act
		var result = defaultInputStringEncryptedWithStringKey.Decrypt<string>(key);

		// Assert
		result.AssertNone().AssertType<IncorrectKeyOrNonceExceptionMsg>();
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
		result.AssertNone().AssertType<UnlockWhenEncryptedContentsIsNoneMsg>();
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
