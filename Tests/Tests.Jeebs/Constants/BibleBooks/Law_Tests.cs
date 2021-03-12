// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests
{
	public class Law_Tests
	{
		[Fact]
		public void Returns_Law_Books()
		{
			// Arrange
			const string? law = "[\"Genesis\",\"Exodus\",\"Leviticus\",\"Numbers\",\"Deuteronomy\"]";

			// Act
			var result = JeebsF.JsonF.Serialise(BibleBooks.Law);

			// Assert
			Assert.Equal(law, result);
		}
	}
}
