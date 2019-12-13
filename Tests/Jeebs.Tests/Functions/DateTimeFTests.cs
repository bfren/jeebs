using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace F
{
	public sealed class DateTimeFTests
	{
		[Fact]
		public void FromUnix_UnixTimestamp_ReturnsDateTime()
		{
			// Arrange
			const int unix = 947001570;
			var expected = new DateTime(2000, 1, 4, 15, 59, 30);

			// Act
			var actual = DateTimeF.FromUnix(unix);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void UnixEpoch_ReturnsUnixEpochAsDateTime()
		{
			// Arrange
			var expected = new DateTime(1970, 1, 1, 0, 0, 0);

			// Act
			var actual = DateTimeF.UnixEpoch();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FromFormat_CorrectInput_ValidFormat_ReturnsDateTime()
		{
			// Arrange
			const string input = "15:59 04/01/2000";
			var expected = new DateTime(2000, 1, 4, 15, 59, 00);
			const string format = "HH:mm dd/MM/yyyy";

			// Act
			var success = DateTimeF.FromFormat(input, format, out DateTime result);

			// Assert
			Assert.True(success);
			Assert.Equal(expected, result);
		}

		[Fact]
		public void FromFormat_CorrectInput_InvalidFormat_ReturnsFalse()
		{
			// Arrange
			const string input = "15:59 04/01/2000";
			const string format = "this is not a valid format";

			// Act
			var result = DateTimeF.FromFormat(input, format, out DateTime _);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void FromFormat_IncorrectInput_ValidFormat_ReturnsFalse()
		{
			// Arrange
			const string input = "15:59:30 01/31/2000";
			const string format = "HH:mm dd/MM/yyyy";

			// Act
			var result = DateTimeF.FromFormat(input, format, out DateTime _);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void FromFormat_IncorrectInput_InvalidFormat_ReturnsFalse()
		{
			// Arrange
			const string input = "15:59:30 01/31/2000";
			const string format = "this is not a valid format";

			// Act
			var result = DateTimeF.FromFormat(input, format, out DateTime _);

			// Assert
			Assert.False(result);
		}
	}
}
