// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateRange_Tests;

public class Includes_Tests
{
	[Fact]
	public void Single_Date_Tests()
	{
		// Arrange
		var start = new DateTime(2000, 1, 1);
		var end = new DateTime(2000, 1, 3);
		var range = new DateRange(start, end);

		// Act
		var inside = range.Includes(new DateTime(2000, 1, 2));
		var justInside1 = range.Includes(start.StartOfDay());
		var justInside2 = range.Includes(end.EndOfDay());
		var outside1 = range.Includes(start.AddDays(-1));
		var outside2 = range.Includes(end.AddDays(1));
		var justOutside1 = range.Includes(start.StartOfDay().AddTicks(-1));
		var justOutside2 = range.Includes(end.EndOfDay().AddTicks(1));

		// Assert
		Assert.True(inside);
		Assert.True(justInside1);
		Assert.True(justInside2);
		Assert.False(outside1);
		Assert.False(outside2);
		Assert.False(justOutside1);
		Assert.False(justOutside2);
	}

	[Fact]
	public void Date_Range_Tests()
	{
		// Arrange
		var start = new DateTime(2000, 1, 1);
		var end = new DateTime(2000, 1, 10);
		var range = new DateRange(start, end);
		var rangeInside = new DateRange(start.AddDays(1), end.AddDays(-1));
		var rangeOutside = new DateRange(start.AddDays(-1), end.AddDays(1));
		var rangeOutsideStart = new DateRange(start.AddDays(-1), end.AddDays(-1));
		var rangeOutsideEnd = new DateRange(start.AddDays(1), end.AddDays(1));

		// Act
		var inside = range.Includes(rangeInside);
		var outside = range.Includes(rangeOutside);
		var outsideStart = range.Includes(rangeOutsideStart);
		var outsideEnd = range.Includes(rangeOutsideEnd);

		// Assert
		Assert.True(inside);
		Assert.False(outside);
		Assert.False(outsideStart);
		Assert.False(outsideEnd);
	}
}
