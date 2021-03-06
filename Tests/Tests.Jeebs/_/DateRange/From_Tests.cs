using System;
using Xunit;

namespace Jeebs.DateRange_Tests
{
	public class From_Tests
	{
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
	}
}
