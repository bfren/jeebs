﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Constants.BibleBooks_Tests;

public class Gospels_Tests
{
	[Fact]
	public void Returns_Gospels()
	{
		// Arrange
		const string? gospels = "[\"Matthew\",\"Mark\",\"Luke\",\"John\"]";

		// Act
		var result = JsonF.Serialise(BibleBooks.Gospels);

		// Assert
		Assert.Equal(gospels, result);
	}
}
