// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.ContentFilters.Blocks.Gallery_Tests;

public class Parse_Tests
{
	public static TheoryData<string, int[], int> Parses_Gallery_Ids_Data() =>
		[
			(
				"<!-- wp:gallery {\"ids\":[917,918,919,922,920,921],\"columns\":3,\"linkTo\":\"file\"} --><!-- /wp:gallery -->",
				new[] { 917, 918, 919, 922, 920, 921 },
				3
			)
		];

	[Theory]
	[MemberData(nameof(Parses_Gallery_Ids_Data))]
	public void Parses_Gallery_Ids(string input, int[] ids, int cols)
	{
		// Arrange
		var expected = $"class=\"hide image-gallery\" data-ids=\"{string.Join(',', ids)}\" data-cols=\"{cols}\"></div>";

		// Act
		var result = Gallery.Parse(input);

		// Assert
		Assert.Contains(expected, result);
	}
}
