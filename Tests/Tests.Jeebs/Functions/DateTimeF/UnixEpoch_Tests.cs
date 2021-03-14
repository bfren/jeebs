// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace F.DateTimeF_Tests
{
	public partial class UnixEpoch_Tests
	{
		[Fact]
		public void ReturnsUnixEpochAsDateTime()
		{
			// Arrange
			var expected = new DateTime(1970, 1, 1, 0, 0, 0);

			// Act
			var actual = DateTimeF.UnixEpoch();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
