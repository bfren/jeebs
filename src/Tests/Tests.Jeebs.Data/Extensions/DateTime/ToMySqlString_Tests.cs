// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Xunit;

namespace Jeebs.Data.DateTimeExtensions_Tests
{
	public class ToMySqlString_Tests
	{
		[Fact]
		public void DateTime_Returns_Valid_MySql_DateTime_String()
		{
			// Arrange
			var year = F.Rnd.NumberF.GetInt32(1, 9999);
			var month = F.Rnd.NumberF.GetInt32(1, 12);
			var day = F.Rnd.NumberF.GetInt32(1, 28);
			var hour = F.Rnd.NumberF.GetInt32(0, 23);
			var minute = F.Rnd.NumberF.GetInt32(0, 59);
			var second = F.Rnd.NumberF.GetInt32(0, 59);
			var dt = new DateTime(year, month, day, hour, minute, second);
			var expected = string.Format(
				"{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}",
				year, month, day, hour, minute, second
			);

			// Act
			var result = dt.ToMySqlString();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
