// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Db.DbConfig_Tests;

public class Key_Tests
{
	[Fact]
	public void Returns_Db_Key()
	{
		// Arrange

		// Act
		var result = DbConfig.Key;

		// Assert
		Assert.Equal(JeebsConfig.Key + ":db", result);
	}
}
