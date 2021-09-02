// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.CryptoF;
using static F.JsonF;

namespace Jeebs.Cryptography.ObjectExtensions_Tests
{
	public partial class Encrypt_Tests
	{
		private readonly string defaultInputString = "String to encrypt.";
		private readonly Foo defaultInputObject = new() { Bar = "Test string." };

		[Theory]
		[InlineData(null)]
		public void Null_Input_Byte_Key_Returns_Empty(string input)
		{
			// Arrange
			var key = GenerateKey().UnsafeUnwrap();

			// Act
			var result = input.Encrypt(key);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(Empty, some);
		}

		[Fact]
		public void String_Input_Byte_Key_Returns_Encrypted_Json()
		{
			// Arrange
			var key = GenerateKey().UnsafeUnwrap();

			// Act
			var result = defaultInputString.Encrypt(key);

			// Assert
			var some = result.AssertSome();
			Assert.NotEqual(defaultInputString, some);
		}

		[Fact]
		public void Object_Input_Byte_Key_Returns_Encrypted_Json()
		{
			// Arrange
			var key = GenerateKey().UnsafeUnwrap();
			var json = Serialise(defaultInputObject);

			// Act
			var result = defaultInputObject.Encrypt(key);

			// Assert
			var some = result.AssertSome();
			Assert.NotEqual(json, some);
		}

		public class Foo
		{
			public string Bar { get; set; } = string.Empty;
		}
	}
}
