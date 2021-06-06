// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.TermList_Tests.Term_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Title()
		{
			// Arrange
			var title = F.Rnd.Str;
			var term = new Term { Title = title };

			// Act
			var result = term.ToString();

			// Assert
			Assert.Equal(title, result);
		}

		public sealed record Term : TermList.Term
		{
			public override string ToString() =>
				base.ToString();
		}
	}
}
