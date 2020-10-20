using System;
using System.Collections.Generic;
using System.Text;
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
