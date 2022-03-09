// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateRange_Tests;

public class StartMustBeBeforeEnd_Tests
{
	[Fact]
	public void Start_May_Equal_End_Tests()
	{
		// Arrange
		var date = new DateTime(2000, 1, 1);

		// Act
		var range = new DateRange(date);

		// Assert
		Assert.Equal(date.StartOfDay(), range.Start);
		Assert.Equal(date.EndOfDay(), range.Finish);
	}

	[Fact]
	public void Start_Before_End_Returns_DateRange()
	{
		// Arrange
		var date1 = new DateTime(2000, 1, 1);
		var date2 = new DateTime(2000, 1, 2);

		// Act
		var result = new DateRange(date1, date2);

		// Assert
		_ = Assert.IsType<DateRange>(result);
	}

	[Fact]
	public void Start_Must_Be_Before_End_Tests()
	{
		// Arrange
		var date1 = new DateTime(2000, 1, 1);
		var date2 = new DateTime(2000, 1, 2);

		// Act
		var incorrect = object () => new DateRange(date2, date1);

		// Assert
		_ = Assert.Throws<ArgumentException>(incorrect);
	}
}
