// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Xunit;

namespace Jeebs.Calendar.VCalendar_Tests;

public class GetHeader_Tests
{
	[Fact]
	public void Returns_Correct_Header_Definition()
	{
		// Arrange
		var expected = new StringBuilder()
			.AppendLine("BEGIN:VCALENDAR")
			.AppendLine("VERSION:2.0")
			.AppendLine("PRODID:-//bfren//NONSGML Jeebs.Calendar//EN")
			.AppendLine("CALSCALE:GREGORIAN")
			.AppendLine("X-PUBLISHED-TTL:PT1H")
			.ToString();

		// Act
		var result = VCalendar.GetHeader();

		// Assert
		Assert.Equal(expected, result);
	}
}
