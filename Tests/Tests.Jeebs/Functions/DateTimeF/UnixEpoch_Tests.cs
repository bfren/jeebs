using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
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
