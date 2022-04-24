// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Azure.DataProtection.DataProtectionConfig_Tests;

public class GetUri_Tests
{
	[Fact]
	public void Returns_Uri__With_Correct_Value()
	{
		// Arrange
		var key = $"https://{Rnd.Str}.com/";
		var config = new DataProtectionConfig { KeyUri = key };

		// Act
		var result = config.GetUri();

		// Assert
		Assert.Equal(key.ToLowerInvariant(), result.AbsoluteUri);
	}
}
