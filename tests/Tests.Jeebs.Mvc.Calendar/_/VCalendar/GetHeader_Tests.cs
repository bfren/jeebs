// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Xunit;

namespace Jeebs.Mvc.Calendar.VCalendar_Tests
{
	public class GetHeader_Tests
	{
		[Fact]
		public void Returns_Correct_Header_Definition()
		{
			// Arrange
			var expected =
				$"BEGIN:VCALENDAR{Environment.NewLine}" +
				$"VERSION:2.0{Environment.NewLine}" +
				$"PRODID:-//bfren//NONSGML Jeebs.Mvc.Calendar//EN{Environment.NewLine}" +
				$"CALSCALE:GREGORIAN{Environment.NewLine}" +
				$"X-PUBLISHED-TTL:PT1H{Environment.NewLine}";

			// Act
			var result = VCalendar.GetHeader();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
