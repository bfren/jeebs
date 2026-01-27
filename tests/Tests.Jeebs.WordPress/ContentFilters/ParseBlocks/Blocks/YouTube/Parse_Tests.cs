// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.ContentFilters.Blocks.YouTube_Tests;

public class Parse_Tests
{
	public static TheoryData<string, string, string> Parses_YouTube_Id_Data() =>
		[
			(
				"<!-- wp:embed {\"url\":\"https://www.youtube.com/watch?v=5k9mk7esVUw\",\"type\":\"video\",\"providerNameSlug\":\"youtube\",\"responsive\":true,\"className\":\"wp-embed-aspect-16-9 wp-has-aspect-ratio\"} --><!-- /wp:embed -->",
				"5k9mk7esVUw",
				"https://www.youtube.com/watch?v=5k9mk7esVUw"
			),
			(
				"<!-- wp:core-embed/youtube {\"url\":\"https://youtu.be/R5OzX-f9ATY\",\"type\":\"video\",\"providerNameSlug\":\"youtube\",\"responsive\":true,\"className\":\"wp-embed-aspect-16-9 wp-has-aspect-ratio\"} --><!-- /wp:core-embed/youtube -->",
				"R5OzX-f9ATY",
				"https://youtu.be/R5OzX-f9ATY"
			)
		];

	[Theory]
	[MemberData(nameof(Parses_YouTube_Id_Data))]
	public void Parses_YouTube_Id(string input, string id, string uri)
	{
		// Arrange
		var expected = $"class=\"hide video-youtube\" data-v=\"{id}\">{uri}</div>";

		// Act
		var result = YouTube.Parse(input);

		// Assert
		Assert.Contains(expected, result);
	}
}
