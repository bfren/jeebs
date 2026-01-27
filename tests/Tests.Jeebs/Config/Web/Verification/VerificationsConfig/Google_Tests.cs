// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Web.Verification.VerificationsConfig_Tests;

public class Google_Tests
{
	[Fact]
	public void Code_Not_Set_Returns_Null()
	{
		// Arrange
		var config = new VerificationConfig();

		// Act
		var result = config.Google;

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Returns_Google_Html()
	{
		// Arrange
		var google = Rnd.Str;
		var config = new VerificationConfig { Google = google };

		// Act
		var result = config.Google;

		// Assert
		Assert.Equal($"google{google}.html", result);
	}
}
