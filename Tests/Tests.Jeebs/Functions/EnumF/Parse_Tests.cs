using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace F
{
	public partial class EnumF_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Parse_NullOrEmpty_Returns_None(string input)
		{
			// Arrange

			// Act
			var result = EnumF.Parse<TestA>(input);

			// Assert
			Assert.IsType<None<TestA>>(result);
		}

		[Fact]
		public void Parse_ValidValue_CorrectType_Returns_Some()
		{
			// Arrange
			const string input = nameof(TestA.Test1);

			// Act
			var result = EnumF.Parse<TestA>(input);

			// Assert
			Assert.Equal(TestA.Test1, result);
		}

		[Fact]
		public void Parse_InvalidValue_CorrectType_Returns_None()
		{
			// Arrange
			const string input = "Test3";

			// Act
			var result = EnumF.Parse<TestA>(input);

			// Assert
			Assert.IsType<None<TestA>>(result);
		}

		[Fact]
		public void Parse_ValidValue_IncorrectType_Returns_None()
		{
			// Arrange
			const string input = nameof(TestA.Test1);

			// Act
			var result = EnumF.Parse(typeof(string), input);

			// Assert
			Assert.IsType<None<object>>(result);
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
