// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.DateTimeInt_Tests;

public class IsValidDateTime_Tests
{
	public static IEnumerable<object[]> Invalid_DateTime_Data()
	{
		yield return new object[] { 10000, 1, 2, 3, 4, nameof(DateTimeInt.Year) };
		yield return new object[] { 2000, 0, 2, 3, 4, nameof(DateTimeInt.Month) };
		yield return new object[] { 2000, 1, 0, 3, 4, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, -1, 1, 2, 3, nameof(DateTimeInt.Month) };
		yield return new object[] { 2000, 13, 1, 2, 3, nameof(DateTimeInt.Month) };
		yield return new object[] { 2000, 1, 32, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2001, 2, 29, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 2, 30, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2004, 2, 30, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2100, 2, 29, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 3, 32, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 4, 31, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 5, 32, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 6, 31, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 7, 32, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 8, 32, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 9, 31, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 10, 32, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 11, 31, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 12, 32, 2, 3, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 1, 1, 24, 30, nameof(DateTimeInt.Hour) };
		yield return new object[] { 2000, 1, 1, 23, 60, nameof(DateTimeInt.Minute) };
		yield return new object[] { -2000, 1, 1, 12, 30, nameof(DateTimeInt.Year) };
		yield return new object[] { 2000, -1, 1, 12, 30, nameof(DateTimeInt.Month) };
		yield return new object[] { 2000, 1, -1, 12, 30, nameof(DateTimeInt.Day) };
		yield return new object[] { 2000, 1, 1, -12, 30, nameof(DateTimeInt.Hour) };
		yield return new object[] { 2000, 1, 1, 12, -30, nameof(DateTimeInt.Minute) };
	}

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
