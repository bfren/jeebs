using System;
using System.Collections.Generic;
using System.Text;
using static F.CryptoF;
using Xunit;

namespace Jeebs.Cryptography.StringExtensions_Tests
{
	public partial class Decrypt_Tests
	{
		private readonly string defaultInputString = "String to encrypt.";
		private readonly string defaultInputStringEncryptedWithByteKey = "{\"salt\":\"EyWHXBL0TyCMvlLXAd5Ecw==\",\"nonce\":\"KrErRaHfQwWdMif7iuO6bMICMljBkdts\",\"encryptedContents\":\"9PlILzx6y4HqaDhHO9ioqW760nqQ+VXqvhlSNd/u89qqG2DS\"}";
		private readonly byte[] defaultByteKey = Convert.FromBase64String("nXhxz39cHyPx3aZmjeXtNEFTRCzjhVlW+6oVPUPtddA=");

		[Theory]
		[InlineData(null)]
		public void Null_Input_Byte_Key_Returns_None(string input)
		{
			// Arrange
			var key = GenerateKey();

			// Act
			var result = input.Decrypt<int>(key);

			// Assert
			var none = Assert.IsAssignableFrom<None<int>>(result);
			Assert.True(none.Reason is Jm.Functions.JsonF.DeserialisingNullOrEmptyStringMsg);
		}

		[Fact]
		public void Invalid_Json_Input_Byte_Key_Returns_None()
		{
			// Arrange
			var key = GenerateKey();
			var json = F.Rnd.Str;

			// Act
			var result = json.Decrypt<int>(key);

			// Assert
			var none = Assert.IsAssignableFrom<None<int>>(result);
			Assert.True(none.Reason is Jm.Functions.JsonF.DeserialiseExceptionMsg);
		}

		[Fact]
		public void Input_Contents_Invalid_Byte_Key_Returns_None()
		{
			// Arrange

			// Act
			var result = defaultInputStringEncryptedWithByteKey.Decrypt<int>(Array.Empty<byte>());

			// Assert
			var none = Assert.IsAssignableFrom<None<int>>(result);
			Assert.True(none.Reason is Jm.Cryptography.Locked.InvalidKeyExceptionMsg);
		}

		[Fact]
		public void Incorrect_Byte_Key_Returns_None()
		{
			// Arrange
			var key = GenerateKey();

			// Act
			var result = defaultInputStringEncryptedWithByteKey.Decrypt<string>(key);

			// Assert
			var none = Assert.IsAssignableFrom<None<string>>(result);
			Assert.True(none.Reason is Jm.Cryptography.Locked.IncorrectKeyOrNonceMsg);
		}

		[Fact]
		public void Incorrect_Json_Input_Byte_Key_Returns_None()
		{
			// Arrange
			var key = GenerateKey();
			const string json = "{\"foo\":\"bar\"}";

			// Act
			var result = json.Decrypt<int>(key);

			// Assert
			var none = Assert.IsAssignableFrom<None<int>>(result);
			Assert.True(none.Reason is Jm.Cryptography.Locked.UnlockWhenEncryptedContentsIsNullMsg);
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
}
