// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace JeebsF.PhpF_Tests
{
	public class Deserialise_Tests
	{
		public const string serialisedArray = @"a:2:{i:1;a:2:{i:1;s:6:""Orange"";i:0;s:5:""Apple"";}i:0;s:12:""Sample Array"";}";

		[Theory]
		[InlineData("b:1;", true)]
		[InlineData("b:0;", false)]
		[InlineData("i:-9223372036854775808;", long.MinValue)]
		[InlineData("i:9223372036854775807;", long.MaxValue)]
		[InlineData("d:-1.7976931348623157E+308;", double.MinValue)]
		[InlineData("d:1.7976931348623157E+308;", double.MaxValue)]
		[InlineData("s:0:\"\";", "")]
		[InlineData("s:15:\"Deserialise_Tests\";", nameof(Deserialise_Tests))]
		[InlineData("N;", "")]
		public void String_Returns_Deserialised_Value<T>(string input, T expected)
			where T : notnull
		{
			// Arrange

			// Act
			var result = PhpF.Deserialise(input);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void String_Returns_Deserialised_Array()
		{
			// Arrange

			// Act
			var result = PhpF.Deserialise(serialisedArray);

			// Assert
			var outer = Assert.IsType<PhpF.AssocArray>(result);
			Assert.Equal("Sample Array", outer[0]);
			var inner = Assert.IsType<PhpF.AssocArray>(outer[1]);
			Assert.Equal("Apple", inner[0]);
			Assert.Equal("Orange", inner[1]);
		}
	}
}
