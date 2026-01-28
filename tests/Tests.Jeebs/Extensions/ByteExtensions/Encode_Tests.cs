// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Extensions.StringExtensions_Tests.Encode_Tests;

namespace Jeebs.Extensions.ByteExtensions_Tests;

public class Encode_Tests
{
	public class With_Null_Input
	{
		[Fact]
		public void Returns_Empty_String()
		{
			// Arrange

			// Act
			var result = ByteExtensions.Encode(null!);

			// Assert
			Assert.Empty(result);
		}
	}

	public class With_Valid_Input
	{
		[Theory]
		[MemberData(nameof(Base64_Input_Expected), MemberType = typeof(StringExtensions_Tests.Encode_Tests))]
		public void Returns_Expected_Encoded_String(string input, string expected)
		{
			// Arrange
			var bytes = System.Text.Encoding.UTF8.GetBytes(input);

			// Act
			var result = bytes.Encode();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
