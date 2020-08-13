using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class DateTimeExtensions_Tests
	{
		[Fact]
		public void LastDayOfWeek_Date_ReturnsOneMinuteToMidnightOnLastDayOfWeek()
		{
			// Arrange
			var date = new DateTime(2000, 1, 4, 15, 59, 30);
			var expected = new DateTime(2000, 1, 8, 23, 59, 59);

			// Act
			var actual = date.LastDayOfWeek();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
