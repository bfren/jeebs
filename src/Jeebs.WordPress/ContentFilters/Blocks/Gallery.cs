// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Jeebs.Functions;
using RndF;

namespace Jeebs.WordPress.ContentFilters.Blocks;

/// <summary>
/// Parse Gallery block.
/// </summary>
internal static partial class Gallery
{
	/// <summary>
	/// Parse WordPress photo gallery.
	/// </summary>
	/// <param name="content">Post content.</param>
	internal static string Parse(string content)
	{
		// Get Gallery info
		var matches = GalleryRegex().Matches(content);
		if (matches.Count == 0)
		{
			return content;
		}

		// Replacement format
		const string format = "<div id=\"{0}\" class=\"hide image-gallery\" data-ids=\"{1}\" data-cols=\"{2}\"></div>";

		// Parse each match
		foreach (var match in matches.Cast<Match>())
		{
			// Info is encoded as JSON so deserialise it first
			var info = match.Groups[1].Value;
			_ = JsonF.Deserialise<GalleryParsed>(info).IfOk(gallery =>
				content = content.Replace(
					match.Value,
					string.Format(CultureInfo.InvariantCulture, format, Rnd.StringF.Get(10), string.Join(",", gallery.Ids), gallery.Columns)
				)
			);
		}

		// Return parsed content
		return content;
	}

	/// <summary>
	/// Used to parse Gallery JSON.
	/// </summary>
	/// <param name="Ids">Image IDs.</param>
	/// <param name="Columns">The number of columns to display.</param>
	private sealed record class GalleryParsed(int[] Ids, int Columns);

	[GeneratedRegex("<!-- wp:gallery ({.*?}) -->(.*?)<!-- /wp:gallery -->", RegexOptions.Singleline)]
	private static partial Regex GalleryRegex();
}
