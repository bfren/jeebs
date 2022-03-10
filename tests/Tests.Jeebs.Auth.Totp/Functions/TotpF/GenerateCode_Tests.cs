// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Totp.Functions.TotpF_Tests;

public class GenerateCode_Tests
{
	[Fact]
	public void Uses_Correct_Period()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var period = Rnd.Int;
		var settings = TotpSettings.Default with { PeriodSeconds = period };
		var interval = TotpF.GetCurrentInterval(period);
		var expected = TotpF.GenerateCode(key, interval, TotpSettings.Default.CodeLength);

		// Act
		var result = TotpF.GenerateCode(key, settings);

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
		var interval = TotpF.GetCurrentInterval(TotpSettings.Default.PeriodSeconds);
		var expected = TotpF.GenerateCode(key, interval, length);

		// Act
		var result = TotpF.GenerateCode(key, settings);

		// Assert
		Assert.Equal(expected, result);
	}
}
