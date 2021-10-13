// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Xunit;

namespace Jeebs.WordPress.Data.ContentFilters.ParseBlocks_Tests;

public class ParseVimeo_Tests
{
	public static IEnumerable<object[]> Parses_Vimeo_Url_Data()
	{
		yield return new object[]
		{
			"<!-- wp:embed {\"url\":\"https://vimeo.com/525453396\",\"type\":\"video\",\"providerNameSlug\":\"vimeo\",\"responsive\":true,\"className\":\"wp-embed-aspect-21-9 wp-has-aspect-ratio\"} --><!-- /wp:embed -->",
			"https://vimeo.com/525453396"
		};

		yield return new object[]
		{
			"<!-- wp:embed {\"url\":\"https://player.vimeo.com/video/525453396\",\"type\":\"video\",\"providerNameSlug\":\"vimeo\",\"responsive\":true,\"className\":\"wp-embed-aspect-16-9 wp-has-aspect-ratio\"} --><!-- /wp:embed -->",
			"https://player.vimeo.com/video/525453396"
		};
	}

	[Theory]
	[MemberData(nameof(Parses_Vimeo_Url_Data))]
	public void Parses_Vimeo_Url(string input, string url)
	{
		// Arrange
		var expected = $"class=\"hide video-vimeo\" data-url=\"{url}\">{url}</div>";

		// Act
		var result = ParseBlocks.ParseVimeo(input);

		// Assert
		Assert.Contains(expected, result);
	}
}
