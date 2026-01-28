// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions.StringExtensions_Tests;

public class Encode_Tests
{
	public static TheoryData<string, string> Base64_Input_Expected =>
		[
			("Hello, World!", "SGVsbG8sIFdvcmxkIQ=="),
			("Jeebs Framework", "SmVlYnMgRnJhbWV3b3Jr"),
			("", ""),
			(" ", "IA=="),
			("1234567890", "MTIzNDU2Nzg5MA=="),
			("Special chars !@#$%^&*()", "U3BlY2lhbCBjaGFycyAhQCMkJV4mKigp"),
			("こんにちは", "44GT44KT44Gr44Gh44Gv"),
			("😊🚀🌟", "8J+YivCfmoDwn4yf")
		];

	public class With_Null_Input
	{
		[Fact]
		public void Returns_Empty_String()
		{
			// Arrange

			// Act
			var result = StringExtensions.Encode(null!);

			// Assert
			Assert.Empty(result);
		}
	}

	public class With_Valid_Input
	{
		[Theory]
		[MemberData(nameof(Base64_Input_Expected), MemberType = typeof(Encode_Tests))]
		public void Returns_Expected_Encoded_String(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.Encode();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
