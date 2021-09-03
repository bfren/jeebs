﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.Table_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Name()
	{
		// Arrange
		var name = F.Rnd.Str;
		var table = Substitute.ForPartsOf<Table>(name);

		// Act
		var result = table.ToString();

		// Assert
		Assert.Equal(name, result);
	}
}