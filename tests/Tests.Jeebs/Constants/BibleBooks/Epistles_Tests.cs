// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Constants.BibleBooks_Tests;

public class Epistles_Tests
{
	[Fact]
	public void Returns_Epistles()
	{
		// Arrange
		const string? epistles = "[\"Romans\",\"1 Corinthians\",\"2 Corinthians\",\"Galatians\",\"Ephesians\",\"Philippians\",\"Colossians\",\"1 Thessalonians\",\"2 Thessalonians\",\"1 Timothy\",\"2 Timothy\",\"Titus\",\"Philemon\",\"Hebrews\",\"James\",\"1 Peter\",\"2 Peter\",\"1 John\",\"2 John\",\"3 John\",\"Jude\",\"Revelation\"]";

		// Act
		var result = JsonF.Serialise(BibleBooks.Epistles);

		// Assert
		Assert.Equal(epistles, result);
	}
}
