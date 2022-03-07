// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using static F.JsonF;

namespace Jeebs.WordPress.Data.ContentFilters;

public sealed partial class ParseBlocks
{
	/// <summary>
	/// Parse embedded YouTube videos
	/// </summary>
	/// <param name="content">Post content</param>
	internal static string ParseYouTube(string content)
	{
		// Get YouTube info
		const string pattern = "<!-- wp:(core-embed/youtube|embed) ({.*?}) -->(.*?)<!-- /wp:(core-embed/youtube|embed) -->";
		var matches = Regex.Matches(content, pattern, RegexOptions.Singleline);
		if (matches.Count == 0)
		{
			return content;
		}

		// Replacement format
		const string format = "<div id=\"{0}\" class=\"hide video-youtube\" data-v=\"{1}\">{2}</div>";

		// Parse each match
		foreach (Match match in matches)
		{
			// Info is encoded as JSON so deserialise it first
			var info = match.Groups[2].Value;
			_ = Deserialise<YouTubeParsed>(info).IfSome(youTube =>
			  {
				  // Get URI
				  var uri = new Uri(youTube.Url);

				  // Get Video ID and replace content using output format
				  if (GetYouTubeVideoId(uri) is string videoId)
				  {
					  content = content.Replace(
						  match.Value,
						  string.Format(CultureInfo.InvariantCulture, format, F.Rnd.StringF.Get(10), videoId, uri)
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
	/// <param name="uri">URI</param>
	internal static string? GetYouTubeVideoId(Uri uri)
	{
		var regex = new Regex(@"^.*(?:(?:youtu\.be\/|v\/|vi\/|u\/\w\/|embed\/)|(?:(?:watch)?\?v(?:i)?=|\&v(?:i)?=))([^#\&\?]*).*");
		var m = regex.Match(uri.AbsoluteUri);

		if (m.Success && m.Groups[1] is Group g && g.Value is string v)
		{
			return v;
		}

		return null;
	}

	/// <summary>
	/// Used to parse YouTube JSON
	/// </summary>
	/// <param name="Url">YouTube URL</param>
	private sealed record class YouTubeParsed(string Url);
}
