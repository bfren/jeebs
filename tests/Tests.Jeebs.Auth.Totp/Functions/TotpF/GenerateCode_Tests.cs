// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Totp;
using Xunit;
using static F.TotpF;

namespace F.TotpF_Tests;

public class GenerateCode_Tests
{
	[Fact]
	public void Uses_Correct_Period()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var period = Rnd.Int;
		var settings = TotpSettings.Default with { PeriodSeconds = period };
		var interval = GetCurrentInterval(period);
		var expected = GenerateCode(key, interval, TotpSettings.Default.CodeLength);

		// Act
		var result = GenerateCode(key, settings);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(6)]
	[InlineData(10)]
	public void Uses_Correct_Code_Length(int length)
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var settings = TotpSettings.Default with { CodeLength = length };
		var interval = GetCurrentInterval(TotpSettings.Default.PeriodSeconds);
		var expected = GenerateCode(key, interval, length);

		// Act
		var result = GenerateCode(key, settings);

		// Assert
		Assert.Equal(expected, result);
	}
}
