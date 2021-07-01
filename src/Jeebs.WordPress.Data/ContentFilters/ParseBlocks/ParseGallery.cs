// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Text.RegularExpressions;
using static F.JsonF;

namespace Jeebs.WordPress.Data.ContentFilters
{
	public sealed partial class ParseBlocks
	{
		/// <summary>
		/// Parse WordPress photo gallery
		/// </summary>
		/// <param name="content">Post content</param>
		internal static string ParseGallery(string content)
		{
			// Get Gallery info
			const string pattern = "<!-- wp:gallery ({.*?}) -->(.*?)<!-- /wp:gallery -->";
			var matches = Regex.Matches(content, pattern, RegexOptions.Singleline);
			if (matches.Count == 0)
			{
				return content;
			}

			// Replacement format
			const string format = "<div id=\"{0}\" class=\"hide image-gallery\" data-ids=\"{1}\" data-cols=\"{2}\"></div>";

			// Parse each match
			foreach (Match match in matches)
			{
				// Info is encoded as JSON so deserialise it first
				var info = match.Groups[1].Value;
				Deserialise<GalleryParsed>(info).IfSome(gallery =>
				{
					content = content.Replace(
						match.Value,
						string.Format(format, F.Rnd.StringF.Get(10), string.Join(",", gallery.Ids), gallery.Columns)
					);
				});
			}

			// Return parsed content
			return content;
		}

		/// <summary>
		/// Used to parse Gallery JSON
		/// </summary>
		/// <param name="Ids">Image IDs</param>
		/// <param name="Columns">The number of columns to display</param>
		private record GalleryParsed(int[] Ids, int Columns);
	}
}
