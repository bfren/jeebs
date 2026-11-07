// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeInt_Tests;

public class IsValidDateTime_Tests
{
	public static TheoryData<int, int, int, int, int, string> Invalid_DateTime_Data() =>
		new()
		{
			{ 10000, 1, 2, 3, 4, nameof(DateTimeInt.Year) },
			{ 2000, 0, 2, 3, 4, nameof(DateTimeInt.Month) },
			{ 2000, 1, 0, 3, 4, nameof(DateTimeInt.Day) },
			{ 2000, -1, 1, 2, 3, nameof(DateTimeInt.Month) },
			{ 2000, 13, 1, 2, 3, nameof(DateTimeInt.Month) },
			{ 2000, 1, 32, 2, 3, nameof(DateTimeInt.Day) },
			{ 2001, 2, 29, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 2, 30, 2, 3, nameof(DateTimeInt.Day) },
			{ 2004, 2, 30, 2, 3, nameof(DateTimeInt.Day) },
			{ 2100, 2, 29, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 3, 32, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 4, 31, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 5, 32, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 6, 31, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 7, 32, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 8, 32, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 9, 31, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 10, 32, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 11, 31, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 12, 32, 2, 3, nameof(DateTimeInt.Day) },
			{ 2000, 1, 1, 24, 30, nameof(DateTimeInt.Hour) },
			{ 2000, 1, 1, 23, 60, nameof(DateTimeInt.Minute) },
			{ -2000, 1, 1, 12, 30, nameof(DateTimeInt.Year) },
			{ 2000, -1, 1, 12, 30, nameof(DateTimeInt.Month) },
			{ 2000, 1, -1, 12, 30, nameof(DateTimeInt.Day) },
			{ 2000, 1, 1, -12, 30, nameof(DateTimeInt.Hour) },
			{ 2000, 1, 1, 12, -30, nameof(DateTimeInt.Minute) }
		};

	[Fact]
	public void Valid_Returns_True()
	{
		// Arrange
		var input = new DateTimeInt(2020, 1, 2, 3, 4);

		// Act
		var (valid, _) = input.IsValidDateTime();

		// Assert
		Assert.True(valid);
	}

	[Theory]
	[InlineData(2001, 1, 31)]
	[InlineData(2001, 2, 28)]
	[InlineData(2000, 2, 29)]
	[InlineData(2004, 2, 29)]
	[InlineData(2001, 3, 31)]
	[InlineData(2001, 4, 30)]
	[InlineData(2001, 5, 31)]
	[InlineData(2001, 6, 30)]
	[InlineData(2001, 7, 31)]
	[InlineData(2001, 8, 31)]
	[InlineData(2001, 9, 30)]
	[InlineData(2001, 10, 31)]
	[InlineData(2001, 11, 30)]
	[InlineData(2001, 12, 31)]
	public void Last_Day_Of_Month_Returns_True(int year, int month, int day)
	{
		// Arrange
		var input = new DateTimeInt(year, month, day, 12, 30);

		// Act
		var (valid, _) = input.IsValidDateTime();

		// Assert
		Assert.True(valid);
	}

	[Theory]
	[MemberData(nameof(Invalid_DateTime_Data))]
	public void Invalid_Returns_False_And_Part(int year, int month, int day, int hour, int minute, string part)
	{
		// Arrange
		var input = new DateTimeInt(year, month, day, hour, minute);

		// Act
		var (valid, invalidPart) = input.IsValidDateTime();

		// Assert
		Assert.False(valid);
		Assert.Equal(part, invalidPart);
	}
}
