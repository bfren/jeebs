// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.Calendar.CalendarBase_Tests
{
	public class DefaultTimeZone_Tests
	{
		[Fact]
		public void Returns_Europe_London()
		{
			// Arrange
			var expected = "Europe/London";

			// Act
			var result = CalendarBase.DefaultTimezone;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
