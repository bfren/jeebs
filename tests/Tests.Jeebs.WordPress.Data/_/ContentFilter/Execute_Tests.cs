// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.ContentFilter_Tests
{
	public class Execute_Tests
	{
		[Fact]
		public void Runs_Filter()
		{
			// Arrange
			var filter = Substitute.For<Func<string, string>>();
			var content = F.Rnd.Str;

			var contentFilter = Substitute.ForPartsOf<ContentFilter>(filter);

			// Act
			_ = contentFilter.Execute(content);

			// Assert
			filter.Received().Invoke(content);
		}
	}
}
