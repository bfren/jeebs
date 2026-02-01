// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Db_Tests;

namespace Jeebs.Data.Common.Db_Tests;

public class Constructor_Tests : Db_Setup
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange

		// Act
		var (db, v) = Setup();

		// Assert
		Assert.Same(v.Client, db.Client);
		Assert.Same(v.Config, db.Config);
		Assert.Same(v.Log, db.LogTest);
	}
}
