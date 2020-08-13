using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class DateTimeExtensions_Tests
	{
		[Fact]
		public void ToStandardString_Date_ReturnsStandardFormattedString()
		{
			// Arrange
			const string expected = "15:59 04/01/2000";
			var date = new DateTime(2000, 1, 4, 15, 59, 30);

			// Act
			var actual = date.ToStandardString();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
