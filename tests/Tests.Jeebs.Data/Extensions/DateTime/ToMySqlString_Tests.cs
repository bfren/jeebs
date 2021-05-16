// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.DateTimeExtensions_Tests
{
	public class ToMySqlString_Tests
	{
		[Fact]
		public void DateTime_Returns_Valid_MySql_DateTime_String()
		{
			// Arrange
			var dt = F.Rnd.DateTime;
			var expected = string.Format(
				"{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}",
				dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second
			);

			// Act
			var result = dt.ToMySqlString();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
