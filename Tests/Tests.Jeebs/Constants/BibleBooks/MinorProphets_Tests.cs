// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests
{
	public class MinorProphets_Tests
	{
		[Fact]
		public void Returns_MinorProphets_Books()
		{
			// Arrange
			const string? prophets = "[\"Hosea\",\"Joel\",\"Amos\",\"Obadiah\",\"Jonah\",\"Micah\",\"Nahum\",\"Habakkuk\",\"Zephaniah\",\"Haggai\",\"Zechariah\",\"Malachi\"]";

			// Act
			var result = JeebsF.JsonF.Serialise(BibleBooks.MinorProphets);

			// Assert
			Assert.Equal(prophets, result);
		}
	}
}
