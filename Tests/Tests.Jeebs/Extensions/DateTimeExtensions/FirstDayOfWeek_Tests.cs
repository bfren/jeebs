using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class DateTimeExtensions_Tests
	{
		[Fact]
		public void FirstDayOfWeek_Date_ReturnsMidnightOnFirstDayOfWeek()
		{
			// Arrange
			var date = new DateTime(2000, 1, 4, 15, 59, 30);
			var expected = new DateTime(2000, 1, 2, 0, 0, 0);

			// Act
			var actual = date.FirstDayOfWeek();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
