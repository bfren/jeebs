// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.ContentFilter_Tests;

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
		_ = filter.Received().Invoke(content);
	}
}
