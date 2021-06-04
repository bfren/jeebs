// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.WordPress.Data.ContentFilters
{
	/// <summary>
	/// Parse Blocks
	/// </summary>
	public sealed partial class ParseBlocks : ContentFilter
	{
		/// <inheritdoc/>
		private ParseBlocks(Func<string, string> filter) : base(filter) { }

		/// <summary>
		/// Create filter
		/// </summary>
		public static ContentFilter Create() =>
			new ParseBlocks(content =>
			{
				// Parse Galleries
				content = ParseGallery(content);

				// Parse YouTube Videos
				content = ParseYouTube(content);

				// Parse Vimeo Videos
				content = ParseVimeo(content);

				// Return parsed content
				return content;
			});
	}
}
