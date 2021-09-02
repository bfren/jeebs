// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests;

public class MinorProphets_Tests
{
	[Fact]
	public void Returns_MinorProphets_Books()
	{
		// Arrange
		const string? prophets = "[\"Hosea\",\"Joel\",\"Amos\",\"Obadiah\",\"Jonah\",\"Micah\",\"Nahum\",\"Habakkuk\",\"Zephaniah\",\"Haggai\",\"Zechariah\",\"Malachi\"]";

		// Act
		var result = F.JsonF.Serialise(BibleBooks.MinorProphets);

		// Assert
		Assert.Equal(prophets, result);
	}
}
