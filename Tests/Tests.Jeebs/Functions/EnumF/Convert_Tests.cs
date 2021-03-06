using Jeebs;
using Xunit;

namespace F.EnumF_Tests
{
	public partial class Convert_Tests
	{
		[Fact]
		public void MatchingValue_ReturnsValue()
		{
			// Arrange
			const TestB input = TestB.Test3;

			// Act
			var result = EnumF.Convert(input).To<TestA>();

			// Assert
			Assert.Equal(TestA.Test1, result);
		}

		[Fact]
		public void NoMatchingValue_Returns_None()
		{
			// Arrange
			const TestB input = TestB.Test5;

			// Act
			var result = EnumF.Convert(input).To<TestA>();

			// Assert
			Assert.IsType<None<TestA>>(result);
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
