using Jeebs;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.DateTimeInt_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Valid_ReturnsStringValue()
		{
			// Arrange
			const string input = "200001020304";

			// Act
			var dt = new DateTimeInt(input);
			var result = dt.ToString();

			// Assert
			Assert.Equal(input, result);
		}
	}
}
