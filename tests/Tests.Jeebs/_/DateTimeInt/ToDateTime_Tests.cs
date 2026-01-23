// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeInt_Tests;

public class ToDateTime_Tests
{
	[Fact]
	public void Valid_DateTime_Returns_Some()
	{
		// Arrange
		var dt = new DateTime(2000, 1, 2, 3, 4, 0);
		var input = new DateTimeInt(dt);

		// Act
		var result = input.ToDateTime();

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(dt, ok);
	}

	[Theory]
	[MemberData(nameof(IsValidDateTime_Tests.Invalid_DateTime_Data), MemberType = typeof(IsValidDateTime_Tests))]
	public void Invalid_DateTime_Returns_None(int year, int month, int day, int hour, int minute, string part)
	{
		// Arrange
		var input = new DateTimeInt(year, month, day, hour, minute);

		// Act
		var result = input.ToDateTime();

		// Assert
		result.AssertFail("Invalid {Part} - 'Y:{Year} M:{Month} D:{Day} H:{Hour} m:{Minute}'.", new { part, year, month, day, hour, minute });
	}
}
