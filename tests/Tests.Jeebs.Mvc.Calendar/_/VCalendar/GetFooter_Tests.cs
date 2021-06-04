﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Mvc.Calendar.VCalendar_Tests
{
	public class GetFooter_Tests
	{
		[Fact]
		public void Returns_Correct_Footer_Definition()
		{
			// Arrange
			var expected = "END:VCALENDAR\r\n";

			// Act
			var result = VCalendar.GetFooter();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
