using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace F
{
	public partial class EnumF_Tests
	{
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
		public void Convert_NoMatchingValue_Returns_None()
		{
			// Arrange
			const TestB input = TestB.Test5;

			// Act
			var result = EnumF.Convert(input).To<TestA>();

			// Assert
			Assert.IsType<None<TestA>>(result);
		}
	}
}
