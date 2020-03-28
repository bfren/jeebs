using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Jeebs.Util;

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
		private static readonly string format = @"<div id=""{0}"" class=""hide video-youtube"" data-v=""{1}"">{2}</div>";

		/// <summary>
		/// Parse content
		/// </summary>
		/// <param name="content">Content to parse</param>
		internal override string Parse(string content)
		{
			// Get YouTube info
			var matches = Regex.Matches(content, "<!-- wp:core-embed/youtube ({.*?}) -->(.*?)<!-- /wp:core-embed/youtube -->", RegexOptions.Singleline);
			if (matches.Count == 0)
			{
				return content;
			}

			// Parse each match
			foreach (Match match in matches)
			{
				// Info is encoded as JSON
				var json = match.Groups[1].Value;
				var youTube = Json.Deserialise<YouTubeParsed>(json);

				// Get URI
				var uri = new Uri(youTube.Url);

				// Get Video ID and replace content using output format
				if (GetVideoId(uri) is string videoId)
				{
					content = content.Replace(match.Value, string.Format(format, F.StringF.Random(10), videoId, uri));
				}
			}

			// Return parsed content
			return content;
		}

		/// <summary>
		/// Get the Video ID based on whether the long or short format has been used
		/// </summary>
		/// <param name="uri">URI</param>
		private string? GetVideoId(Uri uri)
		{
			if (uri.PathAndQuery.Contains("?v=")) // Long link https://youtube.com/?v=xxx
			{
				var parts = HttpUtility.ParseQueryString(uri.Query);
				if (parts["v"] is string v)
				{
					return v;
				}
			}
			else if (uri.Host == "youtu.be") // Short link https://youtu.be/xxx
			{
				var path = uri.GetLeftPart(UriPartial.Path).TrimEnd('/');
				if (path[1..] is string v && !string.IsNullOrEmpty(v))
				{
					return v;
				}
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
