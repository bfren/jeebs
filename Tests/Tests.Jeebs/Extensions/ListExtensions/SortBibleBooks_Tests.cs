// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Linq;
using Xunit;
using static JeebsF.JsonF;

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
			var array = new[] { JeebsF.Rnd.Str, JeebsF.Rnd.Str, JeebsF.Rnd.Str, JeebsF.Rnd.Str };

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
