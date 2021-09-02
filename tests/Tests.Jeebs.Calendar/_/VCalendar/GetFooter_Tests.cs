// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Calendar.VCalendar_Tests
{
	public class GetFooter_Tests
	{
		[Fact]
		public void Returns_Correct_Footer_Definition()
		{
			// Arrange
			var expected = $"END:VCALENDAR{Environment.NewLine}";

			// Act
			var result = VCalendar.GetFooter();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
