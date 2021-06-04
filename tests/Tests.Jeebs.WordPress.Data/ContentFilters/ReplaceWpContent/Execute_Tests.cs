// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.WordPress.Data.ContentFilters.ReplaceWpContent_Tests
{
	public class Execute_Tests
	{
		[Fact]
		public void Replaces_Content()
		{
			// Arrange
			var from = F.Rnd.Str;
			var to = F.Rnd.Str;

			// Act
			var result = ReplaceWpContent.Create(from, to).Execute(from);

			// Assert
			Assert.Equal(to, result);
		}
	}
}
