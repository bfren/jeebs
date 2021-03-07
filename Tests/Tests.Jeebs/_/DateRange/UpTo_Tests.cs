// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.DateRange_Tests
{
	public class UpTo_Tests
	{
		[Fact]
		public void StartIsMinimum_Tests()
		{
			// Arrange
			var date = new DateTime(2000, 1, 1);

			// Act
			var range = DateRange.UpTo(date);

			// Assert
			Assert.Equal(DateTime.MinValue.StartOfDay(), range.Start);
			Assert.Equal(date.EndOfDay(), range.End);
		}
	}
}
