using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Cryptography.Extensions
{
	public sealed class HashingTests
	{
		private readonly string password = "Password to hash.";
		private readonly string passwordHash = "$argon2id$v=19$m=131072,t=6,p=1$hlK04aWKBnSzi2Sx2T3f7Q$YP80Zmw9Ef0oixaawEGtrJtDszIs2VeoCbH0Qmc+fUk\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Hash_String_NullOrEmpty_ThrowsArgumentNullException(string input)
		{
			// Arrange

			// Act
			Action result = () => input.Hash();

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Theory]
		[InlineData(32, "GkAreFLuhzmsSnz2iNbA+l2EhnKPGtd50IQiyVMSdG8=")]
		[InlineData(64, "ryAkj2djPYAPc0IH5zwYH2QdSY/n6kS+ZLc6U96zvb1BNdVEwP7cFcAdzk2+YZMmoGiEbqQFE9QmqlzaCHboVw==")]
		public void Hash_String_ReturnsHashedString(int length, string expected)
		{
			// Arrange
			const string input = "String to hash.";

			// Act
			var hash = input.Hash(length);

			// Assert
			Assert.Equal(hash, expected);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void HashPassword_NullOrEmpty_ThrowsArgumentNullException(string input)
		{
			// Arrange

			// Act
			Action result = () => input.HashPassword();

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Fact]
		public void HashPassword_String_ReturnsUniqueValue()
		{
			// Arrange
			var input = password;

			// Act
			var hash1 = input.HashPassword();
			var hash2 = input.HashPassword();

			// Assert
			Assert.NotEqual(hash1, hash2);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void VerifyPassword_NullOrEmpty_ThrowsArgumentNullException(string input)
		{
			// Arrange
			var password = this.password;

			// Act
			Action result = () => input.VerifyPassword(password);

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Fact]
		public void VerifyPassword_IncorrectPassword_ReturnsFalse()
		{
			// Arrange
			var password = F.StringF.Random(16);
			var passwordHash = this.passwordHash;

			// Act
			var result = passwordHash.VerifyPassword(password);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void VerifyPassword_CorrectPassword_ReturnsTrue()
		{
			// Arrange
			var password = this.password;
			var passwordHash = this.passwordHash;

			// Act
			var result = passwordHash.VerifyPassword(password);

			// Assert
			Assert.True(result);
		}
	}
}
