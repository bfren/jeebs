﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Db.DbConfig_Tests;

public class Authentication_Tests
{
	[Fact]
	public void Returns_Default_If_Not_Set()
	{
		// Arrange
		var name = Rnd.Str;
		var config = new DbConfig { Default = name };

		// Act
		var result = config.Authentication;

		// Assert
		Assert.Equal(name, result);
	}

	[Fact]
	public void Returns_If_Set()
	{
		// Arrange
		var n0 = Rnd.Str;
		var n1 = Rnd.Str;
		var config = new DbConfig
		{
			Default = n0,
			Authentication = n1
		};

		// Act
		var result = config.Authentication;

		// Assert
		Assert.Equal(n1, result);
	}
}
