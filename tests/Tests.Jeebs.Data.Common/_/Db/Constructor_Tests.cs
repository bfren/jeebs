// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Db_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange

		// Act
		var (db, v) = Db_Setup.Get();

		// Assert
		Assert.Same(v.Client, db.Client);
		Assert.Same(v.Config, db.Config);
		Assert.Same(v.Log, db.LogTest);
	}
}
