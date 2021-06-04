// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
				$"PRODID:-//bcg|design//NONSGML Jeebs.Mvc.Calendar//EN{Environment.NewLine}" +
				$"CALSCALE:GREGORIAN{Environment.NewLine}" +
				$"X-PUBLISHED-TTL:PT1H{Environment.NewLine}";

			// Act
			var result = VCalendar.GetHeader();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
