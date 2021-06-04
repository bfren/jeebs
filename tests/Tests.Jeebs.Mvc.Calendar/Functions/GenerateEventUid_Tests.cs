// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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

			// Act
			_ = CalendarF.GenerateEventUid(lastModified, domain);

			// Assert
			Assert.Equal(1, CalendarF.eventCounter);
		}
	}
}
