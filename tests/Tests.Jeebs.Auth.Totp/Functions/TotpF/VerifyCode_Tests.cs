// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Totp.Functions.TotpF_Tests;

public class VerifyCode_Tests
{
	[Fact]
	public void Incorrect_Key_Returns_False()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var code = TotpF.GenerateCode(key, TotpSettings.Default);

		// Act
		var result = TotpF.VerifyCode(Rnd.ByteF.Get(8), code, TotpSettings.Default);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Incorrect_Code_Returns_False()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		_ = TotpF.GenerateCode(key, TotpSettings.Default);

		// Act
		var result = TotpF.VerifyCode(key, Rnd.Str, TotpSettings.Default);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Too_Late_Returns_False()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var interval = TotpF.GetCurrentInterval(30) - 1;
		var code = TotpF.GenerateCode(key, interval, 6);

		// Act
		var result = TotpF.VerifyCode(key, code, TotpSettings.Default with { IntervalTolerance = false });

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Too_Early_Returns_False()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var interval = TotpF.GetCurrentInterval(30) + 1;
		var code = TotpF.GenerateCode(key, interval, 6);

		// Act
		var result = TotpF.VerifyCode(key, code, TotpSettings.Default with { IntervalTolerance = false });

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Correct_Code_Returns_True()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var code = TotpF.GenerateCode(key, TotpSettings.Default);

		// Act
		var result = TotpF.VerifyCode(key, code, TotpSettings.Default);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Late_With_Tolerance_Returns_True()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var interval = TotpF.GetCurrentInterval(30) - 1;
		var code = TotpF.GenerateCode(key, interval, 6);

		// Act
		var result = TotpF.VerifyCode(key, code, TotpSettings.Default with { IntervalTolerance = true });

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Early_With_Tolerance_Returns_True()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var interval = TotpF.GetCurrentInterval(30) + 1;
		var code = TotpF.GenerateCode(key, interval, 6);

		// Act
		var result = TotpF.VerifyCode(key, code, TotpSettings.Default with { IntervalTolerance = true });

		// Assert
		Assert.True(result);
	}
}
