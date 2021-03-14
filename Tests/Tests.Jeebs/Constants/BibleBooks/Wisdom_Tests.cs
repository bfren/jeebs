// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Constants.BibleBooks_Tests
{
	public class Wisdom_Tests
	{
		[Fact]
		public void Returns_Wisdom_Books()
		{
			// Arrange
			const string? wisdom = "[\"Job\",\"Psalms\",\"Proverbs\",\"Ecclesiastes\",\"Song of Songs\"]";

			// Act
			var result = F.JsonF.Serialise(BibleBooks.Wisdom);

			// Assert
			Assert.Equal(wisdom, result);
		}
	}
}
