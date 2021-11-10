// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Xunit;

namespace F.DateTimeF_Tests;

public partial class FromUnix_Tests
{
	[Fact]
	public void UnixTimestamp_ReturnsDateTime()
	{
		// Arrange
		const int unix = 947001570;
		var expected = new DateTime(2000, 1, 4, 15, 59, 30);

		// Act
		var actual = DateTimeF.FromUnix(unix);

		// Assert
		Assert.Equal(expected, actual);
	}
}
