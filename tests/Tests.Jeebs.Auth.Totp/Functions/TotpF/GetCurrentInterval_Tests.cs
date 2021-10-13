// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Xunit;
using static F.TotpF;

namespace F.TotpF_Tests;

public class GetCurrentInterval_Tests
{
	[Theory]
	[InlineData(5)]
	[InlineData(30)]
	[InlineData(60)]
	[InlineData(120)]
	public void Returns_Correct_Interval(int period)
	{
		// Arrange
		var seconds = (ulong)(DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds;
		var expected = seconds / (ulong)period;

		// Act
		var result = GetCurrentInterval(period);

		// Assert
		Assert.Equal(expected, result);
	}
}
