using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.ListExtensions_Tests
{
	public class SortBibleBooks_Tests
	{
		[Fact]
		public void ArrayContainsBibleBooks_ReturnsSortedArray()
		{
			// Arrange
			var books = Constants.BibleBooks.All;

			// Act
			var shuffled = books.ToArray().Shuffle();
			var sorted = shuffled.SortBibleBooks(b => b);

			// Assert
			Assert.NotEqual(books, shuffled);
			Assert.Equal(books, sorted);
		}

		[Fact]
		public void ArrayDoesNotContainBibleBooks_ReturnsOriginalArray()
		{
			// Arrange
			var array = new[] { "one", "two", "three", "four" };

			// Act
			var shuffled = array.Shuffle();
			var sorted = shuffled.SortBibleBooks(b => b);

			// Assert
			Assert.Equal(shuffled, sorted);
		}
	}
}
