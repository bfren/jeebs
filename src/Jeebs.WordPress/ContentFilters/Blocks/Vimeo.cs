// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;

namespace Jeebs.WordPress.ContentFilters.Blocks;

/// <summary>
/// Parse Vimeo blocks.
/// </summary>
internal static class Vimeo
{
	/// <summary>
	/// Parse embedded Vimeo videos
	/// </summary>
	/// <param name="content">Post content</param>
	internal static string Parse(string content) =>
		Embed.Parse(content, Embed.EmbedType.Video, Embed.Provider.Vimeo, (id, embed) =>
			string.Format(CultureInfo.InvariantCulture, "<div id=\"{0}\" class=\"hide video-vimeo\" data-url=\"{1}\">{2}</div>", id, embed.Url, embed.Url)
		);
}
