// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.DateRange_Tests
{
	public class Overlaps_Tests
	{
		[Fact]
		public void Date_Range_Tests()
		{
			// Arrange
			var start = new DateTime(2000, 1, 1);
			var end = new DateTime(2000, 1, 10);
			var range = new DateRange(start, end);
			var rangeOverlapStart = new DateRange(start.AddDays(-1), end.AddDays(-1));
			var rangeOverlapEnd = new DateRange(start.AddDays(1), end.AddDays(1));
			var rangeWithin = new DateRange(start.AddDays(1), end.AddDays(-1));
			var rangeOutside = new DateRange(new DateTime(2001, 1, 1), new DateTime(2001, 1, 2));

			// Act
			var overlapStart = range.Overlaps(rangeOverlapStart);
			var overlapEnd = range.Overlaps(rangeOverlapEnd);
			var overlapWithin = range.Overlaps(rangeWithin);
			var outside = range.Overlaps(rangeOutside);

			// Assert
			Assert.True(overlapStart);
			Assert.True(overlapEnd);
			Assert.True(overlapWithin);
			Assert.False(outside);
		}
	}
}
