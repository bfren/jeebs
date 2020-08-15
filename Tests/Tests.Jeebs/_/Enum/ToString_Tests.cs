using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Enum_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void ReturnsName()
		{
			// Arrange
			const string input = "test";
			var test = new EnumTest(input);

			// Act
			var result = test.ToString();

			// Assert
			Assert.Equal(input, result);
		}

		public class EnumTest : Enum
		{
			public EnumTest(string name) : base(name) { }

			public static readonly EnumTest Test1 = new EnumTest("test1");
			public static readonly EnumTest Test2 = new EnumTest("test2");

			public static Option<EnumTest> Parse(string value)
			{
				return Parse(value, new[] { Test1, Test2 });
			}
		}
	}
}
