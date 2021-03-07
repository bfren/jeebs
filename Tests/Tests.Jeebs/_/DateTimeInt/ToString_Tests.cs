// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
