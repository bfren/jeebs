// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests
{
	public class MajorProphets_Tests
	{
		[Fact]
		public void Returns_MajorProphets_Books()
		{
			// Arrange
			const string? prophets = "[\"Isaiah\",\"Jeremiah\",\"Lamentations\",\"Ezekiel\",\"Daniel\"]";

			// Act
			var result = F.JsonF.Serialise(BibleBooks.MajorProphets);

			// Assert
			Assert.Equal(prophets, result);
		}
	}
}
