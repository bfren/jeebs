// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.Cryptography.StringExtensions_Tests
{
	public sealed class Hash_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Hash_String_NullOrEmpty_Returns_Some_Empty(string input)
		{
			// Arrange

			// Act
			var result = input.Hash();

			// Assert
			var some = result.AssertSome();
			Assert.Equal(string.Empty, some);
		}

		[Theory]
		[InlineData(32, "GkAreFLuhzmsSnz2iNbA+l2EhnKPGtd50IQiyVMSdG8=")]
		[InlineData(64, "ryAkj2djPYAPc0IH5zwYH2QdSY/n6kS+ZLc6U96zvb1BNdVEwP7cFcAdzk2+YZMmoGiEbqQFE9QmqlzaCHboVw==")]
		public void Hash_String_Value_Returns_Some_HashedValue(int length, string expected)
		{
			// Arrange
			const string input = "String to hash.";

			// Act
			var result = input.Hash(length);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(expected, some);
		}
	}
}
