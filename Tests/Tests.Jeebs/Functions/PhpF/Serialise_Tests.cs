// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections;
using Xunit;

namespace F.PhpF_Tests
{
	public class Serialise_Tests
	{
		public const string serialisedArray = @"a:2:{i:1;a:2:{i:1;s:6:""Orange"";i:0;s:5:""Apple"";}i:0;s:12:""Sample Array"";}";

		[Theory]
		[InlineData(true, "b:1;")]
		[InlineData(false, "b:0;")]
		[InlineData(int.MinValue, "i:-2147483648;")]
		[InlineData(int.MaxValue, "i:2147483647;")]
		[InlineData(long.MinValue, "i:-9223372036854775808;")]
		[InlineData(long.MaxValue, "i:9223372036854775807;")]
		[InlineData(float.MinValue, "d:-3.4028235E+38;")]
		[InlineData(float.MaxValue, "d:3.4028235E+38;")]
		[InlineData(double.MinValue, "d:-1.7976931348623157E+308;")]
		[InlineData(double.MaxValue, "d:1.7976931348623157E+308;")]
		[InlineData("", "s:0:\"\";")]
		[InlineData(nameof(Serialise_Tests), "s:15:\"Serialise_Tests\";")]
		public void Value_Returns_Serialised_String<T>(T input, string expected)
		{
			// Arrange

			// Act
			var result = PhpF.Serialise(input);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Array_Returns_Serialised_String()
		{
			// Arrange
			var arrayInner = new Hashtable
			{
				{ 0, "Apple" },
				{ 1, "Orange" }
			};

			var arrayOuter = new Hashtable
			{
				{ 0, "Sample Array" },
				{ 1, arrayInner }
			};

			// Act
			var result = PhpF.Serialise(arrayOuter);

			// Assert
			Assert.Equal(serialisedArray, result);
		}

		[Theory]
		[InlineData(null)]
		public void Null_Returns_Null<T>(T input)
		{
			// Arrange

			// Act
			var result = PhpF.Serialise(input);

			// Assert
			Assert.Equal($"{PhpF.Null};", result);
		}
	}
}
