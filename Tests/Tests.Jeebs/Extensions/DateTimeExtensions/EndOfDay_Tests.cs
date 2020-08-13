using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class DateTimeExtensions_Tests
	{
		[Fact]
		public void EndOfDay_Date_ReturnsOneMinuteToMidnight()
		{
			// Arrange
			var date = new DateTime(2000, 1, 1, 15, 59, 30);
			var expected = new DateTime(2000, 1, 1, 23, 59, 59);

			// Act
			var actual = date.EndOfDay();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
