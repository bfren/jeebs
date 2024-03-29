﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Constants.BibleBooks_Tests;

public class All_Tests
{
	[Fact]
	public void Returns_All_Books()
	{
		// Arrange
		const string? all = "[\"Genesis\",\"Exodus\",\"Leviticus\",\"Numbers\",\"Deuteronomy\",\"Joshua\",\"Judges\",\"Ruth\",\"1 Samuel\",\"2 Samuel\",\"1 Kings\",\"2 Kings\",\"1 Chronicles\",\"2 Chronicles\",\"Ezra\",\"Nehemiah\",\"Esther\",\"Job\",\"Psalms\",\"Proverbs\",\"Ecclesiastes\",\"Song of Songs\",\"Isaiah\",\"Jeremiah\",\"Lamentations\",\"Ezekiel\",\"Daniel\",\"Hosea\",\"Joel\",\"Amos\",\"Obadiah\",\"Jonah\",\"Micah\",\"Nahum\",\"Habakkuk\",\"Zephaniah\",\"Haggai\",\"Zechariah\",\"Malachi\",\"Matthew\",\"Mark\",\"Luke\",\"John\",\"Acts\",\"Romans\",\"1 Corinthians\",\"2 Corinthians\",\"Galatians\",\"Ephesians\",\"Philippians\",\"Colossians\",\"1 Thessalonians\",\"2 Thessalonians\",\"1 Timothy\",\"2 Timothy\",\"Titus\",\"Philemon\",\"Hebrews\",\"James\",\"1 Peter\",\"2 Peter\",\"1 John\",\"2 John\",\"3 John\",\"Jude\",\"Revelation\"]";

		// Act
		var result = JsonF.Serialise(BibleBooks.All);

		// Assert
		Assert.Equal(all, result);
	}
}
