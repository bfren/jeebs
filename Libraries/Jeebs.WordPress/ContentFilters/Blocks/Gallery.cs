// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Text.RegularExpressions;
using JeebsF;
using static JeebsF.JsonF;

namespace Jeebs.WordPress.ContentFilters.Blocks
{
	/// <summary>
	/// Gallery block
	/// </summary>
	internal sealed class Gallery : ParseBlocks.Block
	{
		/// <summary>
		/// Output format
		/// </summary>
		private const string format = @"<div class=""hide image-gallery"" data-ids=""{0}"" data-cols=""{1}""></div>";

		/// <summary>
		/// Parse content
		/// </summary>
		/// <param name="content">Content to parse</param>
		internal override string Parse(string content)
		{
			// Get Gallery info
			var matches = Regex.Matches(content, "<!-- wp:gallery ({.*?}) -->(.*?)<!-- /wp:gallery -->", RegexOptions.Singleline);
			if (matches.Count == 0)
			{
				return content;
			}

			// Parse each match
			foreach (Match match in matches)
			{
				// Info is encoded as JSON
				var json = match.Groups[1].Value;
				if (Deserialise<GalleryParsed>(json) is Some<GalleryParsed> gallery)
				{
					// Replace content using output format
					content = content.Replace(
						match.Value,
						string.Format(format, string.Join(",", gallery.Value.Ids), gallery.Value.Columns)
					);
				}
			}

			// Return parsed content
			return content;
		}

		/// <summary>
		/// Used to parse Gallery JSON
		/// </summary>
		private class GalleryParsed
		{
			/// <summary>
			/// Image IDs
			/// </summary>
			public int[] Ids { get; set; } = Array.Empty<int>();

			/// <summary>
			/// Number of columns
			/// </summary>
			public int Columns { get; set; }
		}
	}
}
