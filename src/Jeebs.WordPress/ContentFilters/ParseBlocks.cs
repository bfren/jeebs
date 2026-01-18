// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.WordPress.ContentFilters;

/// <summary>
/// Parse Blocks.
/// </summary>
public sealed class ParseBlocks : ContentFilter
{
	/// <inheritdoc/>
	private ParseBlocks(Func<string, string> filter) : base(filter) { }

	/// <summary>
	/// Create filter.
	/// </summary>
	public static ContentFilter Create() =>
		new ParseBlocks(content =>
		{
			// Parse Galleries
			content = Blocks.Gallery.Parse(content);

			// Parse YouTube Videos
			content = Blocks.YouTube.Parse(content);

			// Parse Vimeo Videos
			content = Blocks.Vimeo.Parse(content);

			// Return parsed content
			return content;
		});
}
