using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Cryptography.StringExtensions_Tests
{
	public sealed class Hash_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Hash_String_NullOrEmpty_Returns_Empty(string input)
		{
			// Arrange

			// Act
			var result = input.Hash();

			// Assert
			Assert.Equal(string.Empty, result);
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
	}
}
