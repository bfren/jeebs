// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeExtensions_Tests;

public class ToStandardString_Tests
{
	[Fact]
	public void Date_ReturnsStandardFormattedString()
	{
		// Arrange
		var date = Rnd.DateTime;
		var expected = date.ToString("HH:mm dd/MM/yyyy");

		// Act
		var result = date.ToStandardString();

		// Assert
		Assert.Equal(expected, result);
	}
}
