using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Enumerated_Tests
{
	public class Parse_Tests
	{
		[Fact]
		public void ValidString_Returns_Some()
		{
			// Arrange
			var input = EnumeratedTest.Test1.ToString();

			// Act
			var result = EnumeratedTest.Parse(input);

			// Assert
			var success = Assert.IsType<Some<EnumeratedTest>>(result);
			Assert.Equal(EnumeratedTest.Test1, success.Value);
		}

		[Fact]
		public void InvalidString_Returns_None()
		{
			// Arrange
			var input = F.Rnd.Str;

			// Act
			var result = EnumeratedTest.Parse(input);

			// Assert
			Assert.IsType<None<EnumeratedTest>>(result);
		}
	}

	public class EnumeratedTest : Enumerated
	{
		public EnumeratedTest(string name) : base(name) { }

		public static readonly EnumeratedTest Test1 = new EnumeratedTest("test1");
		public static readonly EnumeratedTest Test2 = new EnumeratedTest("test2");

		public static Option<EnumeratedTest> Parse(string value)
		{
			return Parse(value, new[] { Test1, Test2 });
		}
	}
}
