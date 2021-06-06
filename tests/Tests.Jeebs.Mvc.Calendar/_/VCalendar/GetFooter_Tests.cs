// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Xunit;

namespace Jeebs.Mvc.Calendar.VCalendar_Tests
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
