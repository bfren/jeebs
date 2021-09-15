// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Xunit;

namespace Jeebs.WordPress.Data.ContentFilters.ParseBlocks_Tests;

public class GetYouTubeVideoId_Tests
{
	/// <summary>
	/// Test URLs from https://stackoverflow.com/a/27728417/8199362
	/// </summary>
	public static IEnumerable<object[]> Returns_Correct_Id_Data()
	{
		yield return new object[] { "https://www.youtube-nocookie.com/embed/up_lNV-yoK4?rel=0", "up_lNV-yoK4" };
		yield return new object[] { "https://www.youtube.com/user/Scobleizer#p/u/1/1p3vcRhsYGo", "1p3vcRhsYGo" };
		yield return new object[] { "https://www.youtube.com/watch?v=cKZDdG9FTKY&feature=channel", "cKZDdG9FTKY" };
		yield return new object[] { "https://www.youtube.com/watch?v=yZ-K7nCVnBI&playnext_from=TL&videos=osPknwzXEas&feature=sub", "yZ-K7nCVnBI" };
		yield return new object[] { "https://www.youtube.com/ytscreeningroom?v=NRHVzbJVx8I", "NRHVzbJVx8I" };
		yield return new object[] { "https://www.youtube.com/user/SilkRoadTheatre#p/a/u/2/6dwqZw0j_jY", "6dwqZw0j_jY" };
		yield return new object[] { "https://youtu.be/6dwqZw0j_jY", "6dwqZw0j_jY" };
		yield return new object[] { "https://www.youtube.com/watch?v=6dwqZw0j_jY&feature=youtu.be", "6dwqZw0j_jY" };
		yield return new object[] { "https://youtu.be/afa-5HQHiAs", "afa-5HQHiAs" };
		yield return new object[] { "https://www.youtube.com/user/Scobleizer#p/u/1/1p3vcRhsYGo?rel=0", "1p3vcRhsYGo" };
		yield return new object[] { "https://www.youtube.com/watch?v=cKZDdG9FTKY&feature=channel", "cKZDdG9FTKY" };
		yield return new object[] { "https://www.youtube.com/watch?v=yZ-K7nCVnBI&playnext_from=TL&videos=osPknwzXEas&feature=sub", "yZ-K7nCVnBI" };
		yield return new object[] { "https://www.youtube.com/ytscreeningroom?v=NRHVzbJVx8I", "NRHVzbJVx8I" };
		yield return new object[] { "https://www.youtube.com/embed/nas1rJpm7wY?rel=0", "nas1rJpm7wY" };
		yield return new object[] { "https://www.youtube.com/watch?v=peFZbP64dsU", "peFZbP64dsU" };
		yield return new object[] { "https://youtube.com/v/dQw4w9WgXcQ?feature=youtube_gdata_player", "dQw4w9WgXcQ" };
		yield return new object[] { "https://youtube.com/vi/dQw4w9WgXcQ?feature=youtube_gdata_player", "dQw4w9WgXcQ" };
		yield return new object[] { "https://youtube.com/?v=dQw4w9WgXcQ&feature=youtube_gdata_player", "dQw4w9WgXcQ" };
		yield return new object[] { "https://www.youtube.com/watch?v=dQw4w9WgXcQ&feature=youtube_gdata_player", "dQw4w9WgXcQ" };
		yield return new object[] { "https://youtube.com/?vi=dQw4w9WgXcQ&feature=youtube_gdata_player", "dQw4w9WgXcQ" };
		yield return new object[] { "https://youtube.com/watch?v=dQw4w9WgXcQ&feature=youtube_gdata_player", "dQw4w9WgXcQ" };
		yield return new object[] { "https://youtube.com/watch?vi=dQw4w9WgXcQ&feature=youtube_gdata_player", "dQw4w9WgXcQ" };
		yield return new object[] { "https://youtu.be/dQw4w9WgXcQ?feature=youtube_gdata_player", "dQw4w9WgXcQ" };
	}

	[Theory]
	[MemberData(nameof(Returns_Correct_Id_Data))]
	public void Returns_Correct_Id(string input, string expected)
	{
		// Arrange
		var uri = new Uri(input);

		// Act
		var result = ParseBlocks.GetYouTubeVideoId(uri);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Incorrect_Url_Returns_Null()
	{
		// Arrange
		var uri = new Uri($"https://{F.Rnd.Str}.com");

		// Act
		var result = ParseBlocks.GetYouTubeVideoId(uri);

		// Assert
		Assert.Null(result);
	}
}
