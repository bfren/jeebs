// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.ContentFilters.ParseBlocks_Tests
{
	public class ParseGallery_Tests
	{
		public static IEnumerable<object[]> Parses_Gallery_Ids_Data()
		{
			yield return new object[]
			{
				"<!-- wp:gallery {\"ids\":[917,918,919,922,920,921],\"columns\":3,\"linkTo\":\"file\"} --><!-- /wp:gallery -->",
				new[] { 917, 918, 919, 922, 920, 921 },
				3
			};
		}

		[Theory]
		[MemberData(nameof(Parses_Gallery_Ids_Data))]
		public void Parses_Gallery_Ids(string input, int[] ids, int cols)
		{
			// Arrange
			var expected = $"class=\"hide image-gallery\" data-ids=\"{string.Join(',', ids)}\" data-cols=\"{cols}\"></div>";

			// Act
			var result = ParseBlocks.ParseGallery(input);

			// Assert
			Assert.Contains(expected, result);
		}
	}
}
