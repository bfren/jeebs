using Jeebs;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.DateTimeInt_Tests
{
	public class IsValidDateTime_Tests
	{
		[Fact]
		public void ValidDateTime_ReturnsDateTime()
		{
			// Arrange
			var input = new DateTime(2000, 1, 2, 3, 4, 0);

			// Act
			var dt = new DateTimeInt(input);
			var result = dt.IsValidDateTime(out DateTime? valid);

			// Assert
			Assert.True(result);
			Assert.NotNull(valid);
			Assert.True(valid.HasValue);
#pragma warning disable CS8629 // Nullable value type may be null.
			Assert.Equal(input, valid.Value);
#pragma warning restore CS8629 // Nullable value type may be null.
		}

		[Theory]
		[InlineData(0, 1, 2, 3, 4)]
		[InlineData(10000, 1, 2, 3, 4)]
		[InlineData(2000, 0, 2, 3, 4)]
		[InlineData(2000, 1, 0, 3, 4)]
		[InlineData(2000, -1, 1, 2, 3)]
		[InlineData(2000, 13, 1, 2, 3)]
		[InlineData(2000, 1, 32, 2, 3)]
		[InlineData(2000, 2, 30, 2, 3)]
		[InlineData(2000, 3, 32, 2, 3)]
		[InlineData(2000, 4, 31, 2, 3)]
		[InlineData(2000, 5, 32, 2, 3)]
		[InlineData(2000, 6, 31, 2, 3)]
		[InlineData(2000, 7, 32, 2, 3)]
		[InlineData(2000, 8, 32, 2, 3)]
		[InlineData(2000, 9, 31, 2, 3)]
		[InlineData(2000, 10, 32, 2, 3)]
		[InlineData(2000, 11, 31, 2, 3)]
		[InlineData(2000, 12, 32, 2, 3)]
		public void IsValidDateTime_InvalidDateTime_ReturnsNull(int year, int month, int day, int hour, int minute)
		{
			// Arrange
			var input = new DateTimeInt(year, month, day, hour, minute);

			// Act
			var result = input.IsValidDateTime(out DateTime? invalid);

			// Assert
			Assert.False(result);
			Assert.Null(invalid);
		}
	}
}
