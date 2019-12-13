using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace F
{
	public sealed class EnumFTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Parse_NullOrEmpty_ThrowsParseException(string input)
		{
			// Arrange

			// Act
			Action result = () => EnumF.Parse<TestA>(input);

			// Assert
			Assert.Throws<Jx.ParseException>(result);
		}

		[Fact]
		public void Parse_ValidValue_CorrectType_ReturnsValue()
		{
			// Arrange
			const string input = nameof(TestA.Test1);

			// Act
			var result = EnumF.Parse<TestA>(input);

			// Assert
			Assert.Equal(TestA.Test1, result);
		}

		[Fact]
		public void Parse_InvalidValue_CorrectType_ThrowsParseException()
		{
			// Arrange
			const string input = "Test3";

			// Act
			Action result = () => EnumF.Parse<TestA>(input);

			// Assert
			Assert.Throws<Jx.ParseException>(result);
		}

		[Fact]
		public void Parse_ValidValue_IncorrectType_ThrowsArgumentException()
		{
			// Arrange
			var input = nameof(TestA.Test1);

			// Act
			Action result = () => EnumF.Parse(typeof(string), input);

			// Assert
			Assert.Throws<ArgumentException>(result);
		}

		[Fact]
		public void Convert_MatchingValue_ReturnsValue()
		{
			// Arrange
			const TestB input = TestB.Test3;

			// Act
			var result = EnumF.Convert(input).To<TestA>();

			// Assert
			Assert.Equal(TestA.Test1, result);
		}

		[Fact]
		public void Convert_NoMatchingValue_ThrowsException()
		{
			// Arrange
			const TestB input = TestB.Test5;

			// Act
			//var a = F.EnumF.Parse<TestA>(input);
			Action result = () => EnumF.Convert(input).To<TestA>();

			// Assert
			Assert.Throws<Jx.ParseException>(result);
		}

		public enum TestA
		{
			Test1,
			Test2
		}

		public enum TestB
		{
			Test3,
			Test4,
			Test5
		}
	}
}
