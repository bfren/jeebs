using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class DateTimeExtensions_Tests
	{
		[Fact]
		public void LastDayOfMonth_Date_ReturnsOneMinuteToMidnightOnLastDayOfMonth()
		{
			// Arrange
			var date = new DateTime(2000, 1, 4, 15, 59, 30);
			var expected = new DateTime(2000, 1, 31, 23, 59, 59);

			// Act
			var actual = date.LastDayOfMonth();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
