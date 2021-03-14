// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.DateTimeExtensions_Tests
{
	public class StartOfDay_Tests
	{
		[Fact]
		public void Date_ReturnsMidnight()
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
