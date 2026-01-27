// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Cryptography.ObjectExtensions_Tests;

public partial class Encrypt_Tests
{
	private readonly string defaultInputString = "String to encrypt.";
	private readonly Foo defaultInputObject = new() { Bar = "Test string." };

	[Fact]
	public void Null_Input_Byte_Key_Returns_Empty()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = ObjectExtensions.Encrypt<string>(null!, key);

		// Assert
		result.AssertOk(JsonF.Empty);
	}

	[Fact]
	public void String_Input_Byte_Key_Returns_Encrypted_Json()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);

		// Act
		var result = defaultInputString.Encrypt(key);

		// Assert
		var ok = result.AssertOk();
		Assert.NotEqual(defaultInputString, ok);
	}

	[Fact]
	public void Object_Input_Byte_Key_Returns_Encrypted_Json()
	{
		// Arrange
		var key = Rnd.ByteF.Get(32);
		var json = JsonF.Serialise(defaultInputObject);

		// Act
		var result = defaultInputObject.Encrypt(key);

		// Assert
		var ok = result.AssertOk();
		Assert.NotEqual(json, ok);
	}

	public class Foo
	{
		public string Bar { get; set; } = string.Empty;
	}
}
