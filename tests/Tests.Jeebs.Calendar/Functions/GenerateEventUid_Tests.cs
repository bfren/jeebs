// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Calendar.Functions.CalendarF_Tests;

public class GenerateEventUid_Tests
{
	[Fact]
	public void Generates_Correct_Uid_Without_Domain()
	{
		// Arrange
		var lastModified = Rnd.DateTime;
		var counter = Rnd.Int;
		var expected = @$"{lastModified:yyyyMMdd\THHmmss}-{counter:000000}";

		// Act
		var result = CalendarF.GenerateEventUid(counter, lastModified);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Generates_Correct_Uid_With_Domain()
	{
		// Arrange
		var lastModified = Rnd.DateTime;
		var counter = Rnd.Int;
		var domain = Rnd.Str;
		var expected = @$"{lastModified:yyyyMMdd\THHmmss}-{counter:000000}@{domain}";

		// Act
		var result = CalendarF.GenerateEventUid(counter, lastModified, domain);

		// Assert
		Assert.Equal(expected, result);
	}
}
