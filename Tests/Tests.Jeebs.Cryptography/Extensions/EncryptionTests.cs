using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Jeebs.Util;
using Xunit;

namespace Jeebs.Cryptography.Extensions
{
	public class EncryptionTests
	{
		private readonly string defaultInputString = "String to encrypt.";
		private readonly string defaultInputStringEncrypted = "{\"salt\":\"EyWHXBL0TyCMvlLXAd5Ecw==\",\"nonce\":\"KrErRaHfQwWdMif7iuO6bMICMljBkdts\",\"encryptedValue\":\"9PlILzx6y4HqaDhHO9ioqW760nqQ+VXqvhlSNd/u89qqG2DS\"}";
		private readonly Foo defaultInputObject = new Foo { Bar = "Test string." };
		private readonly string defaultInputObjectEncrypted = "{\"salt\":\"JS6bvJ43YwdyUs8Un5r96w==\",\"nonce\":\"NnWOGcT8xQwM4f/Ugoz/Vi5mXkCZM48Z\",\"encryptedValue\":\"QSf2lzMmItFNpGkAy0eWRalr8iQuNghMdWH9mJC3Of6vLFKQsy0=\"}";
		private readonly byte[] defaultKey = Convert.FromBase64String("nXhxz39cHyPx3aZmjeXtNEFTRCzjhVlW+6oVPUPtddA=");

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Encrypt_NullOrEmpty_ThrowsArgumentNullException(string input)
		{
			// Arrange
			var key = F.CryptoF.GenerateKey();

			// Act
			Action result = () => input.Encrypt(key);

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Fact]
		public void Encrypt_String_ReturnsEncryptedString()
		{
			// Arrange
			var input = defaultInputString;
			var key = F.CryptoF.GenerateKey();

			// Act
			var result = input.Encrypt(key);

			// Assert
			Assert.NotEqual(input, result);
		}

		[Fact]
		public void Encrypt_Object_ReturnsEncryptedString()
		{
			// Arrange
			var input = defaultInputObject;
			var key = F.CryptoF.GenerateKey();

			// Act
			var json = Json.Serialise(input);
			var result = input.Encrypt(key);

			// Assert
			Assert.NotEqual(json, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Decrypt_NullOrEmpty_ThrowsArgumentNullException(string input)
		{
			// Arrange
			var key = F.CryptoF.GenerateKey();

			// Act
			Action result = () => input.Decrypt(key);

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Fact]
		public void Decrypt_InvalidJson_ThrowsDeserialisationException()
		{
			// Arrange
			const string input = "this is invalid json";
			var key = F.CryptoF.GenerateKey();

			// Act
			Action result = () => input.Decrypt(key);

			// Assert
			Assert.Throws<Jx.Cryptography.DeserialisationException>(result);
		}

		[Fact]
		public void Decrypt_IncorrectJson_ThrowsDecryptionException()
		{
			// Arrange
			const string input = "{\"foo\":\"bar\"}";
			var key = F.CryptoF.GenerateKey();

			// Act
			Action result = () => input.Decrypt(key);

			// Assert
			Assert.Throws<Jx.Cryptography.DecryptionException>(result);
		}

		[Fact]
		public void Decrypt_BoxContainsInvalidJson_ThrowsDecryptionException()
		{
			// Arrange
			var input = defaultInputStringEncrypted;
			var key = defaultKey;

			// Act
			Action result = () => input.Decrypt<Foo>(key);

			// Assert
			Assert.Throws<Jx.Cryptography.DecryptionException>(result);
		}

		[Fact]
		public void Decrypt_WithIncorrectKey_ThrowsCryptographicException()
		{
			// Arrange
			var input = defaultInputStringEncrypted;
			var key = F.CryptoF.GenerateKey();

			// Act
			Action result = () => input.Decrypt(key);

			// Assert
			Assert.Throws<CryptographicException>(result);
		}

		[Fact]
		public void Decrypt_ValidJson_ReturnsDecryptedString()
		{
			// Arrange
			var input = defaultInputStringEncrypted;
			var key = defaultKey;

			// Act
			var result = input.Decrypt(key);

			// Assert
			Assert.Equal(defaultInputString, result);
		}

		[Fact]
		public void Decrypt_ValidJson_ReturnsDecryptedObject()
		{
			// Arrange
			var input = defaultInputObjectEncrypted;
			var key = defaultKey;

			// Act
			var result = input.Decrypt<Foo>(key);

			// Assert
			Assert.Equal(defaultInputObject, result, new FooComparer());
		}

		public class Foo
		{
			public string Bar { get; set; } = string.Empty;
		}

		public class FooComparer : IEqualityComparer<Foo>
		{
			public bool Equals(Foo? x, Foo? y) => x?.Bar == y?.Bar;

			public int GetHashCode(Foo obj) => obj.GetHashCode();
		}
	}
}
