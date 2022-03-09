// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Random;
using Maybe.Testing;
using Xunit;
using static Jeebs.Cryptography.Locked.M;
using static Jeebs.Functions.JsonF.M;

namespace Jeebs.Cryptography.StringExtensions_Tests;

public partial class Decrypt_Tests
{
	private readonly string defaultInputStringEncryptedWithStringKey = "{\"value\":{},\"encryptedContents\":\"RKDdTvdBrxf28cMuuBF+mKkVkYEhJSgwnCnTprGtHeeIMr56\",\"salt\":\"kJ3HSzbuEssDYpGmK9ix1A==\",\"nonce\":\"ehg2foprhsqf7UTrBRpU0cjWvkK0sn/f\"}";
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
		var none = result.AssertNone();
		_ = Assert.IsType<DeserialisingNullOrEmptyStringMsg>(none);
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
		var none = result.AssertNone();
		_ = Assert.IsType<DeserialiseExceptionMsg>(none);
	}

	[Fact]
	public void Input_Contents_Invalid_String_Key_Returns_None()
	{
		// Arrange

		// Act
		var result = defaultInputStringEncryptedWithStringKey.Decrypt<int>(string.Empty);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<IncorrectKeyOrNonceExceptionMsg>(none);
	}

	[Fact]
	public void Incorrect_String_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.Str;

		// Act
		var result = defaultInputStringEncryptedWithStringKey.Decrypt<string>(key);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<IncorrectKeyOrNonceExceptionMsg>(none);
	}

	[Fact]
	public void Incorrect_Json_Input_String_Key_Returns_None()
	{
		// Arrange
		var key = Rnd.Str;
		const string json = "{\"foo\":\"bar\"}";

		// Act
		var result = json.Decrypt<int>(key);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnlockWhenEncryptedContentsIsNoneMsg>(none);
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
