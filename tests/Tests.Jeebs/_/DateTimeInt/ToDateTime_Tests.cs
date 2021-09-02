// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static Jeebs.DateTimeInt.Msg;

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
		var some = result.AssertSome();
		Assert.Equal(dt, some);
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
		var none = result.AssertNone();
		var msg = Assert.IsType<InvalidDateTimeMsg>(none);
		Assert.Equal(part, msg.Value.part);
	}
}
