// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static F.JsonF;

namespace Jeebs.Cryptography.ObjectExtensions_Tests
{
	public partial class Encrypt_Tests
	{
		private readonly string defaultStringKey = "nXhxz39cHyPx3a";

		[Theory]
		[InlineData(null)]
		public void Null_Input_String_Key_Returns_Empty(string input)
		{
			// Arrange

			// Act
			var result = input.Encrypt(defaultStringKey);

			// Assert
			Assert.Equal(Empty, result);
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
			var json = Serialise(defaultInputObject);
			var result = defaultInputObject.Encrypt(defaultStringKey);

			// Assert
			Assert.NotEqual(json, result);
		}
	}
}
