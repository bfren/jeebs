// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Cryptography.ObjectExtensions_Tests;

public partial class Encrypt_Tests
{
	private readonly string defaultStringKey = "nXhxz39cHyPx3a";

	[Fact]
	public void Null_Input_String_Key_Returns_Empty()
	{
		// Arrange

		// Act
		var result = ObjectExtensions.Encrypt<string>(null!, defaultStringKey);

		// Assert
		Assert.Equal(JsonF.Empty, result);
	}

	[Fact]
	public void String_Input_String_Key_Returns_Encrypted_Json()
	{
		// Arrange

		// Act
		var result = defaultInputString.Encrypt(defaultStringKey);

		// Assert
		Assert.NotEqual(defaultInputString, result);
	}

	[Fact]
	public void Object_Input_String_Key_Returns_Encrypted_Json()
	{
		// Arrange

		// Act
		var json = JsonF.Serialise(defaultInputObject);
		var result = defaultInputObject.Encrypt(defaultStringKey);

		// Assert
		Assert.NotEqual(json, result);
	}
}
