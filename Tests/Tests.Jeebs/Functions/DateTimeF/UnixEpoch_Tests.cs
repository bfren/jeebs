using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace F
{
	public partial class DateTimeF_Tests
	{
		[Fact]
		public void UnixEpoch_ReturnsUnixEpochAsDateTime()
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
