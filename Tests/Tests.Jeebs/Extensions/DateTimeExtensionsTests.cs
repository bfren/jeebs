using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Extensions
{
	public sealed class DateTimeExtensionsTests
	{
		[Fact]
		public void StartOfDay_Date_ReturnsMidnight()
		{
			// Arrange
			var date = new DateTime(2000, 1, 1, 15, 59, 30);
			var expected = new DateTime(2000, 1, 1, 0, 0, 0);

			// Act
			var actual = date.StartOfDay();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void EndOfDay_Date_ReturnsOneMinuteToMidnight()
		{
			// Arrange
			var date = new DateTime(2000, 1, 1, 15, 59, 30);
			var expected = new DateTime(2000, 1, 1, 23, 59, 59);

			// Act
			var actual = date.EndOfDay();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FirstDayOfWeek_Date_ReturnsMidnightOnFirstDayOfWeek()
		{
			// Arrange
			var date = new DateTime(2000, 1, 4, 15, 59, 30);
			var expected = new DateTime(2000, 1, 2, 0, 0, 0);

			// Act
			var actual = date.FirstDayOfWeek();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void LastDayOfWeek_Date_ReturnsOneMinuteToMidnightOnLastDayOfWeek()
		{
			// Arrange
			var date = new DateTime(2000, 1, 4, 15, 59, 30);
			var expected = new DateTime(2000, 1, 8, 23, 59, 59);

			// Act
			var actual = date.LastDayOfWeek();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FirstDayOfMonth_Date_ReturnsMidnightOnFirstDayOfMonth()
		{
			// Arrange
			var date = new DateTime(2000, 1, 4, 15, 59, 30);
			var expected = new DateTime(2000, 1, 1, 0, 0, 0);

			// Act
			var actual = date.FirstDayOfMonth();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void LastDayOfMonth_Date_ReturnsOneMinuteToMidnightOnLastDayOfMonth()
		{
			// Arrange
			var date = new DateTime(2000, 1, 4, 15, 59, 30);
			var expected = new DateTime(2000, 1, 31, 23, 59, 59);

			// Act
			var actual = date.LastDayOfMonth();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToStandardString_Date_ReturnsStandardFormattedString()
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
