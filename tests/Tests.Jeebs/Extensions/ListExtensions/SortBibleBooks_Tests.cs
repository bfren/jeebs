// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Xunit;
using static F.JsonF;

namespace Jeebs.ListExtensions_Tests;

public class SortBibleBooks_Tests
{
	[Fact]
	public void ArrayContainsBibleBooks_ReturnsSortedArray()
	{
		// Arrange
		var books = Constants.BibleBooks.All;
		var shuffled = books.ToArray().Shuffle().ToList();

		// Act
		shuffled.SortBibleBooks(b => b);

		// Assert
		Assert.Equal(books, shuffled);
	}

	[Fact]
	public void ArrayDoesNotContainBibleBooks_ReturnsOriginalArray()
	{
		// Arrange
		var array = new[] { F.Rnd.Str, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str };
		var shuffled = array.Shuffle().ToList();
		var originalOrder = Serialise(shuffled);

		// Act
		shuffled.SortBibleBooks(b => b);

		// Assert
		Assert.Equal(originalOrder, Serialise(shuffled));
	}
}
