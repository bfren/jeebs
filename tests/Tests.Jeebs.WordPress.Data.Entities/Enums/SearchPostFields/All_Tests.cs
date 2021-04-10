// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Enums.SearchPostFields_Tests
{
	public class All_Tests
	{
		[Fact]
		public void Zero_Returns_None()
		{
			// Arrange

			// Act
			var result = (SearchPostFields)0;

			// Assert
			Assert.Equal(SearchPostFields.None, result);
		}

		[Theory]
		[InlineData(SearchPostFields.Title)]
		[InlineData(SearchPostFields.Slug)]
		[InlineData(SearchPostFields.Content)]
		[InlineData(SearchPostFields.Excerpt)]
		public void Matches_All_Fields(SearchPostFields field)
		{
			// Arrange

			// Act
			var result = SearchPostFields.All & field;

			// Assert
			Assert.Equal(field, result);
		}
	}
}
