// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests;

public class NewTestament_Tests
{
	[Fact]
	public void Returns_NewTestament_Books()
	{
		// Arrange
		const string? nt = "[\"Matthew\",\"Mark\",\"Luke\",\"John\",\"Acts\",\"Romans\",\"1 Corinthians\",\"2 Corinthians\",\"Galatians\",\"Ephesians\",\"Philippians\",\"Colossians\",\"1 Thessalonians\",\"2 Thessalonians\",\"1 Timothy\",\"2 Timothy\",\"Titus\",\"Philemon\",\"Hebrews\",\"James\",\"1 Peter\",\"2 Peter\",\"1 John\",\"2 John\",\"3 John\",\"Jude\",\"Revelation\"]";

		// Act
		var result = F.JsonF.Serialise(BibleBooks.NewTestament);

		// Assert
		Assert.Equal(nt, result);
	}
}
