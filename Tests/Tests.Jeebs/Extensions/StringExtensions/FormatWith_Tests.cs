using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class FormatWith_Tests
	{
		[Fact]
		public void Replace_Ordered_Numbered_Values()
		{
			// Arrange
			const string format = "{0} , {1} , {2}";
			var values = new[] { 3, 4, 5 };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal("3 , 4 , 5", result);
		}

		[Fact]
		public void Replace_Unordered_Numbered_Values()
		{
			// Arrange
			const string format = "{1} , {0} , {2}";
			var values = new[] { 3, 4, 5 };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal("3 , 4 , 5", result);
		}

		[Fact]
		public void Replace_Named_Values()
		{
			// Arrange
			const string format = "{zero} , {@one} , {two}";
			var values = new { zero = 3, one = 4, two = 5 };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal("3 , 4 , 5", result);
		}

		[Fact]
		public void Replace_Array_Values()
		{
			// Arrange
			const string format = "{zero} , {one} , {two:0.0}";
			var zero = 3;
			var one = "four";
			var two = 5.0d;
			var values = new object[] { zero, one, two };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal("3 , four , 5.0", result);
		}

		[Fact]
		public void Replace_Numbered_Values_With_Formats()
		{
			// Arrange
			const string format = "{0:00} , {1:0.00} , {2:0,000.0}";
			var values = new[] { 3, 4, 5 };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal("03 , 4.00 , 0,005.0", result);
		}

		[Fact]
		public void Replace_Named_Values_With_Formats()
		{
			// Arrange
			const string format = "{zero:00} , {one:0.00} , {two:0,000.0}";
			var values = new { zero = 3, one = 4, two = 5 };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal("03 , 4.00 , 0,005.0", result);
		}

		[Fact]
		public void Replace_Array_Values_With_Formats()
		{
			// Arrange
			const string format = "{zero:00} , {one:0.00} , {2:0,000.0}";
			var zero = 3;
			var one = 4;
			var two = 5;
			var values = new object[] { zero, one, two };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal("03 , 4.00 , 0,005.0", result);
		}

		[Fact]
		public void Replace_Values_When_Format_Named_Values_Numbered()
		{
			// Arrange
			const string format = "{zero} , {one} , {two}";
			var values = new[] { 3, 4, 5 };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal("3 , 4 , 5", result);
		}

		[Fact]
		public void Replace_Values_When_Format_Mixed_Values_Numbered()
		{
			// Arrange
			const string format = "{zero} , {0} , {1}";
			var values = new[] { 3, 4, 5 };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal("3 , 4 , 5", result);
		}

		[Fact]
		public void Return_Format_When_Format_Numbered_Values_Named()
		{
			// Arrange
			const string format = "{0} , {1} , {2}";
			var values = new { zero = 3, one = 4, two = 5 };

			// Act
			var result = format.FormatWith(values);

			// Assert
			Assert.Equal(format, result);
		}
	}
}
