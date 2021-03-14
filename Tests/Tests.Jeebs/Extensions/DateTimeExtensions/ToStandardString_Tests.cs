// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.DateTimeExtensions_Tests
{
	public class ToStandardString_Tests
	{
		[Fact]
		public void Date_ReturnsStandardFormattedString()
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
