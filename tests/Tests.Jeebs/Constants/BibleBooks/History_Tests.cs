// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests;

public class History_Tests
{
	[Fact]
	public void Returns_History_Books()
	{
		// Arrange
		const string? history = "[\"Joshua\",\"Judges\",\"Ruth\",\"1 Samuel\",\"2 Samuel\",\"1 Kings\",\"2 Kings\",\"1 Chronicles\",\"2 Chronicles\",\"Ezra\",\"Nehemiah\",\"Esther\"]";

		// Act
		var result = F.JsonF.Serialise(BibleBooks.History);

		// Assert
		Assert.Equal(history, result);
	}
}
