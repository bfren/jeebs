// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;
using static F.CryptoF;
using static F.JsonF.Msg;
using static Jeebs.Cryptography.Locked.Msg;

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
			var key = GenerateKey().UnsafeUnwrap();

			// Act
			var result = input.Decrypt<int>(key);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<DeserialisingNullOrEmptyStringMsg>(none);
		}

		[Fact]
		public void Invalid_Json_Input_Byte_Key_Returns_None()
		{
			// Arrange
			var key = GenerateKey().UnsafeUnwrap();
			var json = F.Rnd.Str;

			// Act
			var result = json.Decrypt<int>(key);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<DeserialiseExceptionMsg>(none);
		}

		[Fact]
		public void Input_Contents_Invalid_Byte_Key_Returns_None()
		{
			// Arrange

			// Act
			var result = defaultInputStringEncryptedWithByteKey.Decrypt<int>(Array.Empty<byte>());

			// Assert
			var none = result.AssertNone();
			Assert.IsType<InvalidKeyExceptionMsg>(none);
		}

		[Fact]
		public void Incorrect_Byte_Key_Returns_None()
		{
			// Arrange
			var key = GenerateKey().UnsafeUnwrap();

			// Act
			var result = defaultInputStringEncryptedWithByteKey.Decrypt<string>(key);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<IncorrectKeyOrNonceExceptionMsg>(none);
		}

		[Fact]
		public void Incorrect_Json_Input_Byte_Key_Returns_None()
		{
			// Arrange
			var key = GenerateKey().UnsafeUnwrap();
			const string json = "{\"foo\":\"bar\"}";

			// Act
			var result = json.Decrypt<int>(key);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnlockWhenEncryptedContentsIsNoneMsg>(none);
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
