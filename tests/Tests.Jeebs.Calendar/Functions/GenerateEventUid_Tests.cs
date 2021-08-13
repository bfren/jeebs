﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace F.MvcF.CalendarF_Tests
{
	public class GenerateEventUid_Tests
	{
		[Fact]
		public void Generates_Correct_Uid()
		{
			// Arrange
			var lastModified = Rnd.DateTime;
			var domain = Rnd.Str;
			var expected = @$"{lastModified:yyyyMMdd\THHmmss}-000000@{domain}";
			CalendarF.EventCounter = 0;

			// Act
			var result = CalendarF.GenerateEventUid(lastModified, domain);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Counter_Incrememts_Correctly()
		{
			// Arrange
			var lastModified = Rnd.DateTime;
			var domain = Rnd.Str;
			CalendarF.EventCounter = 0;

			// Act
			_ = CalendarF.GenerateEventUid(lastModified, domain);

			// Assert
			Assert.Equal(1, CalendarF.EventCounter);
		}
	}
}
