using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Cryptography
{
	public sealed class HashPassword_Tests
	{
		private readonly string password = "Password to hash.";

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
	}
}
