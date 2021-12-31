// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Xunit;

namespace F.DateTimeF_Tests;

public partial class UnixEpoch_Tests
{
	[Fact]
	public void ReturnsUnixEpochAsDateTime()
	{
		// Arrange
		var expected = new DateTime(1970, 1, 1, 0, 0, 0);

		// Act
		var result = DateTimeF.UnixEpoch();

		// Assert
		Assert.Equal(expected, result);
	}
}
