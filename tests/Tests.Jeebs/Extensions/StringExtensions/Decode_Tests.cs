// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions.StringExtensions_Tests;

public class Decode_Tests
{
	public class With_Null_Input
	{
		[Fact]
		public void Returns_Empty_String()
		{
			// Arrange

			// Act
			var result = StringExtensions.Decode(null!);

			// Assert
			Assert.Empty(result);
		}
	}

	public class With_Valid_Input
	{
		[Theory]
		[MemberData(nameof(Encode_Tests.Base64_Input_Expected), MemberType = typeof(Encode_Tests))]
		public void Returns_Expected_Decoded_String(string expected, string input)
		{
			// Arrange

			// Act
			var result = input.Decode();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
