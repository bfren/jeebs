using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public sealed class DateRangeTests
	{
		[Fact]
		public void StartEqualsEnd_Tests()
		{
			// Arrange
			var date = new DateTime(2000, 1, 1);

			// Act
			var range = new DateRange(date);

			// Assert
			Assert.Equal(date.StartOfDay(), range.Start);
			Assert.Equal(date.EndOfDay(), range.End);
		}

		[Fact]
		public void StartMustBeBeforeEnd_Tests()
		{
			// Arrange
			var date1 = new DateTime(2000, 1, 1);
			var date2 = new DateTime(2000, 1, 2);

			// Act
			Func<DateRange> correct = () => new DateRange(date1, date2);
			Action incorrect = () => new DateRange(date2, date1);

			// Assert
			Assert.IsType<DateRange>(correct());
			Assert.Throws<ArgumentException>(incorrect);
		}

		[Fact]
		public void EndIsMaximum_Tests()
		{
			// Arrange
			var date = new DateTime(2000, 1, 1);

			// Act
			var range = DateRange.From(date);

			// Assert
			Assert.Equal(date, range.Start);
			Assert.Equal(DateTime.MaxValue.EndOfDay(), range.End);
		}

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

		[Fact]
		public void Includes_Tests()
		{
			// Arrange
			var start = new DateTime(2000, 1, 1);
			var end = new DateTime(2000, 1, 3);
			var range = new DateRange(start, end);

			// Act
			var inside = range.Includes(new DateTime(2000, 1, 2));
			var justInside1 = range.Includes(start.StartOfDay());
			var justInside2 = range.Includes(end.EndOfDay());
			var outside1 = range.Includes(start.AddDays(-1));
			var outside2 = range.Includes(end.AddDays(1));
			var justOutside1 = range.Includes(start.StartOfDay().AddTicks(-1));
			var justOutside2 = range.Includes(end.EndOfDay().AddTicks(1));

			// Assert
			Assert.True(inside);
			Assert.True(justInside1);
			Assert.True(justInside2);
			Assert.False(outside1);
			Assert.False(outside2);
			Assert.False(justOutside1);
			Assert.False(justOutside1);
		}

		[Fact]
		public void IncludesRange_Tests()
		{
			// Arrange
			var start = new DateTime(2000, 1, 1);
			var end = new DateTime(2000, 1, 10);
			var range = new DateRange(start, end);
			var rangeInside = new DateRange(start.AddDays(1), end.AddDays(-1));
			var rangeOutside = new DateRange(start.AddDays(-1), end.AddDays(1));
			var rangeOutsideStart = new DateRange(start.AddDays(-1), end.AddDays(-1));
			var rangeOutsideEnd = new DateRange(start.AddDays(1), end.AddDays(1));

			// Act
			var inside = range.Includes(rangeInside);
			var outside = range.Includes(rangeOutside);
			var outsideStart = range.Includes(rangeOutsideStart);
			var outsideEnd = range.Includes(rangeOutsideEnd);

			// Assert
			Assert.True(inside);
			Assert.False(outside);
			Assert.False(outsideStart);
			Assert.False(outsideEnd);
		}

		[Fact]
		public void Overlaps_Tests()
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
