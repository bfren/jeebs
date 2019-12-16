using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Extensions
{
	public sealed class ListExtensionsTests
	{
		[Fact]
		public void SortBibleBooks_ArrayContainsBibleBooks_ReturnsSortedArray()
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
		public void SortBibleBooks_ArrayDoesNotContainBibleBooks_ReturnsOriginalArray()
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
