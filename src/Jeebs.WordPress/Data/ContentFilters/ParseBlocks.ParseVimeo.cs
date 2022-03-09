// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;

namespace Jeebs.WordPress.Data.ContentFilters;

public sealed partial class ParseBlocks
{
	/// <summary>
	/// Parse embedded Vimeo videos
	/// </summary>
	/// <param name="content">Post content</param>
	internal static string ParseVimeo(string content) =>
		ParseEmbed(content, EmbedType.Video, Provider.Vimeo, (id, embed) =>
			string.Format(CultureInfo.InvariantCulture, "<div id=\"{0}\" class=\"hide video-vimeo\" data-url=\"{1}\">{2}</div>", id, embed.Url, embed.Url)
		);
}
