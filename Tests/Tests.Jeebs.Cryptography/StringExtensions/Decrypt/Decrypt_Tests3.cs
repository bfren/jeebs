using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Cryptography.StringExtensions_Tests
{
	public partial class Decrypt_Tests
	{
		[Theory]
		[InlineData(null)]
		public void Without_Null_Input_String_Key_Returns_Empty(string input)
		{
			// Arrange
			var key = F.Rnd.String;

			// Act
			var result = input.Decrypt(key);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Without_Invalid_Json_Input_String_Key_Returns_Empty()
		{
			// Arrange
			var key = F.Rnd.String;
			var json = F.Rnd.String;

			// Act
			var result = json.Decrypt(key);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Without_Input_Contents_Invalid_String_Key_Returns_Empty()
		{
			// Arrange

			// Act
			var result = defaultInputStringEncryptedWithStringKey.Decrypt(string.Empty);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Without_Incorrect_String_Key_Returns_Empty()
		{
			// Arrange
			var key = F.Rnd.String;

			// Act
			var result = defaultInputStringEncryptedWithStringKey.Decrypt(key);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Without_Incorrect_Json_Input_String_Key_Returns_Empty()
		{
			// Arrange
			var key = F.Rnd.String;
			const string json = "{\"foo\":\"bar\"}";

			// Act
			var result = json.Decrypt(key);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Without_Valid_Json_Input_Correct_String_Key_Returns_Value()
		{
			// Arrange

			// Act
			var result = defaultInputStringEncryptedWithStringKey.Decrypt(defaultStringKey);

			// Assert
			Assert.Equal(defaultInputString, result);
		}
	}
}
