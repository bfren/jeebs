// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.Enums.SearchPostFields_Tests
{
	public class All_Tests
	{
		[Fact]
		public void Zero_Returns_None()
		{
			// Arrange

			// Act
			var result = (SearchPostField)0;

			// Assert
			Assert.Equal(SearchPostField.None, result);
		}

		[Theory]
		[InlineData(SearchPostField.Title)]
		[InlineData(SearchPostField.Slug)]
		[InlineData(SearchPostField.Content)]
		[InlineData(SearchPostField.Excerpt)]
		public void Matches_All_Fields(SearchPostField field)
		{
			// Arrange

			// Act
			var result = SearchPostField.All & field;

			// Assert
			Assert.Equal(field, result);
		}
	}
}
