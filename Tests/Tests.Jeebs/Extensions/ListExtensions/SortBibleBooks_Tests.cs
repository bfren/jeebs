using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static F.JsonF;

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
			var shuffled = books.ToArray().Shuffle().ToList();
			Assert.NotEqual(books, shuffled);

			shuffled.SortBibleBooks(b => b);
			Assert.Equal(books, shuffled);

			// Assert
		}

		[Fact]
		public void ArrayDoesNotContainBibleBooks_ReturnsOriginalArray()
		{
			// Arrange
			var array = new[] { F.Rnd.Str, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str };

			// Act
			var shuffled = array.Shuffle().ToList();
			var order0 = Serialise(shuffled);

			shuffled.SortBibleBooks(b => b);
			var order1 = Serialise(shuffled);

			// Assert
			Assert.Equal(order0, order1);
		}
	}
}
