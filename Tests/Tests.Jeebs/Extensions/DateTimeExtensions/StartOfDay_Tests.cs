using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class DateTimeExtensions_Tests
	{
		[Fact]
		public void StartOfDay_Date_ReturnsMidnight()
		{
			// Arrange
			var date = new DateTime(2000, 1, 1, 15, 59, 30);
			var expected = new DateTime(2000, 1, 1, 0, 0, 0);

			// Act
			var actual = date.StartOfDay();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
