// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests
{
	public class Gospels_Tests
	{
		[Fact]
		public void Returns_Gospels()
		{
			// Arrange
			const string? gospels = "[\"Matthew\",\"Mark\",\"Luke\",\"John\"]";

			// Act
			var result = JeebsF.JsonF.Serialise(BibleBooks.Gospels);

			// Assert
			Assert.Equal(gospels, result);
		}
	}
}
