// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Xunit;

namespace Jeebs.WordPress.Data.ContentFilters.ParseBlocks_Tests
{
	public class ParseYouTube_Tests
	{
		public static IEnumerable<object[]> Parses_YouTube_Id_Data()
		{
			yield return new object[]
			{
				"<!-- wp:embed {\"url\":\"https://www.youtube.com/watch?v=5k9mk7esVUw\",\"type\":\"video\",\"providerNameSlug\":\"youtube\",\"responsive\":true,\"className\":\"wp-embed-aspect-16-9 wp-has-aspect-ratio\"} --><!-- /wp:embed -->",
				"5k9mk7esVUw",
				"https://www.youtube.com/watch?v=5k9mk7esVUw"
			};

			yield return new object[]
			{
				"<!-- wp:embed {\"url\":\"https://youtu.be/R5OzX-f9ATY\",\"type\":\"video\",\"providerNameSlug\":\"youtube\",\"responsive\":true,\"className\":\"wp-embed-aspect-16-9 wp-has-aspect-ratio\"} --><!-- /wp:embed -->",
				"R5OzX-f9ATY",
				"https://youtu.be/R5OzX-f9ATY"
			};
		}

		[Theory]
		[MemberData(nameof(Parses_YouTube_Id_Data))]
		public void Parses_YouTube_Id(string input, string id, string uri)
		{
			// Arrange
			var expected = $"class=\"hide video-youtube\" data-v=\"{id}\">{uri}</div>";

			// Act
			var result = ParseBlocks.ParseYouTube(input);

			// Assert
			Assert.Contains(expected, result);
		}
	}
}
