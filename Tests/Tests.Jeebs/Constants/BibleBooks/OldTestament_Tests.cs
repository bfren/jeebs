// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests
{
	public class OldTestament_Tests
	{
		[Fact]
		public void Returns_OldTestament_Books()
		{
			// Arrange
			const string? ot = "[\"Genesis\",\"Exodus\",\"Leviticus\",\"Numbers\",\"Deuteronomy\",\"Joshua\",\"Judges\",\"Ruth\",\"1 Samuel\",\"2 Samuel\",\"1 Kings\",\"2 Kings\",\"1 Chronicles\",\"2 Chronicles\",\"Ezra\",\"Nehemiah\",\"Esther\",\"Job\",\"Psalms\",\"Proverbs\",\"Ecclesiastes\",\"Song of Songs\",\"Isaiah\",\"Jeremiah\",\"Lamentations\",\"Ezekiel\",\"Daniel\",\"Hosea\",\"Joel\",\"Amos\",\"Obadiah\",\"Jonah\",\"Micah\",\"Nahum\",\"Habakkuk\",\"Zephaniah\",\"Haggai\",\"Zechariah\",\"Malachi\"]";

			// Act
			var result = F.JsonF.Serialise(BibleBooks.OldTestament);

			// Assert
			Assert.Equal(ot, result);
		}
	}
}
