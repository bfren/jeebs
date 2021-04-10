// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Text.RegularExpressions;

namespace Jeebs.WordPress.ContentFilters.Blocks
{
	/// <summary>
	/// YouTube block
	/// </summary>
	internal sealed class YouTube : ParseBlocks.Block
	{
		/// <summary>
		/// Output format
		/// </summary>
		private const string format = @"<div id=""{0}"" class=""hide video-youtube"" data-v=""{1}"">{2}</div>";

		/// <summary>
		/// Parse content
		/// </summary>
		/// <param name="content">Content to parse</param>
		internal override string Parse(string content)
		{
			// Get YouTube info
			var matches = Regex.Matches(content, "<!-- wp:(core-embed/youtube|embed) ({.*?}) -->(.*?)<!-- /wp:(core-embed/youtube|embed) -->", RegexOptions.Singleline);
			if (matches.Count == 0)
			{
				return content;
			}

			// Parse each match
			foreach (Match match in matches)
			{
				// Info is encoded as JSON
				var json = match.Groups[2].Value;
				if (F.JsonF.Deserialise<YouTubeParsed>(json) is Some<YouTubeParsed> youTube)
				{
					// Get URI
					var uri = new Uri(youTube.Value.Url);

					// Get Video ID and replace content using output format
					if (GetVideoId(uri) is string videoId)
					{
						content = content.Replace(
							match.Value,
							string.Format(format, F.Rnd.StringF.Get(10), videoId, uri)
						);
					}
				}
			}

			// Return parsed content
			return content;
		}

		/// <summary>
		/// Get the Video ID based on whether the long or short format has been used
		/// Regex comes from https://stackoverflow.com/a/27728417/8199362
		/// </summary>
		/// <param name="uri">URI</param>
		private static string? GetVideoId(Uri uri)
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
		private class YouTubeParsed
		{
			/// <summary>
			/// YouTube Url
			/// </summary>
			public string Url { get; set; } = string.Empty;
		}
	}
}
