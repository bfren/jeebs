// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Jeebs.Functions;
using RndF;

namespace Jeebs.WordPress.ContentFilters.Blocks;

/// <summary>
/// Parse YouTube blocks.
/// </summary>
internal static partial class YouTube
{
	/// <summary>
	/// Parse embedded YouTube videos.
	/// </summary>
	/// <param name="content">Post content.</param>
	internal static string Parse(string content)
	{
		// Get YouTube info
		var matches = YouTubeRegex().Matches(content);
		if (matches.Count == 0)
		{
			return content;
		}

		// Replacement format
		const string format = "<div id=\"{0}\" class=\"hide video-youtube\" data-v=\"{1}\">{2}</div>";

		// Parse each match
		foreach (var match in matches.Cast<Match>())
		{
			// Info is encoded as JSON so deserialise it first
			var info = match.Groups[2].Value;
			_ = JsonF.Deserialise<YouTubeParsed>(info).IfSome(youTube =>
			{
				// Get URI
				var uri = new Uri(youTube.Url);

				// Get Video ID and replace content using output format
				if (GetVideoId(uri) is string videoId)
				{
					content = content.Replace(
						match.Value,
						string.Format(CultureInfo.InvariantCulture, format, Rnd.StringF.Get(10), videoId, uri)
					);
				}
			});
		}

		// Return parsed content
		return content;
	}

	/// <summary>
	/// Get the Video ID based on whether the long or short format has been used
	/// Regex comes from https://stackoverflow.com/a/27728417/8199362
	/// </summary>
	/// <param name="uri">URI.</param>
	internal static string? GetVideoId(Uri uri)
	{
		var regex = VideoRegex();
		var m = regex.Match(uri.AbsoluteUri);

		if (m.Success && m.Groups[1] is Group g && g.Value is string v)
		{
			return v;
		}

		return null;
	}

	/// <summary>
	/// Used to parse YouTube JSON.
	/// </summary>
	/// <param name="Url">YouTube URL.</param>
	private sealed record class YouTubeParsed(string Url);

	[GeneratedRegex("<!-- wp:(core-embed/youtube|embed) ({.*?}) -->(.*?)<!-- /wp:(core-embed/youtube|embed) -->", RegexOptions.Singleline)]
	private static partial Regex YouTubeRegex();

	[GeneratedRegex("^.*(?:(?:youtu\\.be\\/|v\\/|vi\\/|u\\/\\w\\/|embed\\/)|(?:(?:watch)?\\?v(?:i)?=|\\&v(?:i)?=))([^#\\&\\?]*).*")]
	private static partial Regex VideoRegex();
}
