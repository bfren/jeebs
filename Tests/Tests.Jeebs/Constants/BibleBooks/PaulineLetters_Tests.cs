// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests
{
	public class PaulineLetters_Tests
	{
		[Fact]
		public void Returns_PaulineLetters()
		{
			// Arrange
			const string? pauline = "[\"Romans\",\"1 Corinthians\",\"2 Corinthians\",\"Galatians\",\"Ephesians\",\"Philippians\",\"Colossians\",\"1 Thessalonians\",\"2 Thessalonians\",\"1 Timothy\",\"2 Timothy\",\"Titus\",\"Philemon\"]";

			// Act
			var result = JeebsF.JsonF.Serialise(BibleBooks.PaulineLetters);

			// Assert
			Assert.Equal(pauline, result);
		}
	}
}
