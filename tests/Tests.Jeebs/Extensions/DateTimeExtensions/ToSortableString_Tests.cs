// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeExtensions_Tests;

public class ToSortableString_Tests
{
	[Fact]
	public void Date_Returns_Sortable_Formatted_String()
	{
		// Arrange
		var date = Rnd.DateTime;
		var expected = date.ToString("yyyy-MM-dd HH:mm:ss.ffff");

		// Act
		var result = date.ToSortableString();

		// Assert
		Assert.Equal(expected, result);
	}
}
