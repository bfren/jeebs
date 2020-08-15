using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.DateRange_Tests
{
	public class StartMustBeBeforeEnd_Tests
	{
		[Fact]
		public void Start_May_Equal_End_Tests()
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
		public void Start_Must_Be_Before_End_Tests()
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
	}
}
