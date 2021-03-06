using System;
using Jeebs;
using Xunit;

namespace F.DateTimeF_Tests
{
	public partial class FromFormat_Tests
	{
		[Fact]
		public void CorrectInput_ValidFormat_ReturnsDateTime()
		{
			// Arrange
			const string input = "15:59 04/01/2000";
			var expected = new DateTime(2000, 1, 4, 15, 59, 00);
			const string format = "HH:mm dd/MM/yyyy";

			// Act
			var result = DateTimeF.FromFormat(input, format);

			// Assert
			var success = Assert.IsType<Some<DateTime>>(result);
			Assert.Equal(expected, success.Value);
		}

		[Fact]
		public void CorrectInput_InvalidFormat_ReturnsFalse()
		{
			// Arrange
			const string input = "15:59 04/01/2000";
			const string format = "this is not a valid format";

			// Act
			var result = DateTimeF.FromFormat(input, format);

			// Assert
			Assert.IsType<None<DateTime>>(result);
		}

		[Fact]
		public void IncorrectInput_ValidFormat_ReturnsFalse()
		{
			// Arrange
			const string input = "15:59:30 01/31/2000";
			const string format = "HH:mm dd/MM/yyyy";

			// Act
			var result = DateTimeF.FromFormat(input, format);

			// Assert
			Assert.IsType<None<DateTime>>(result);
		}

		[Fact]
		public void IncorrectInput_InvalidFormat_ReturnsFalse()
		{
			// Arrange
			const string input = "15:59:30 01/31/2000";
			const string format = "this is not a valid format";

			// Act
			var result = DateTimeF.FromFormat(input, format);

			// Assert
			Assert.IsType<None<DateTime>>(result);
		}
	}
}
