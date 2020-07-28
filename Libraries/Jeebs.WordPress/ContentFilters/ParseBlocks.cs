using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.WordPress.ContentFilters.Blocks;

namespace Jeebs.WordPress.ContentFilters
{
	/// <summary>
	/// Parse Blocks
	/// </summary>
	public sealed class ParseBlocks : ContentFilter
	{
		/// <inheritdoc/>
		private ParseBlocks(Func<string, string> filter) : base(filter) { }

		/// <summary>
		/// Create filter
		/// </summary>
		public static ContentFilter Create()
			=> new ParseBlocks(content =>
			{
				// Parse Galleries
				content = new Gallery().Parse(content);

				// Parse YouTube Videos
				content = new YouTube().Parse(content);

				// Return parsed content
				return content;
			});

		/// <summary>
		/// Block
		/// </summary>
		internal abstract class Block
		{
			/// <summary>
			/// Parse content
			/// </summary>
			/// <param name="content">Content to parse</param>
			internal abstract string Parse(string content);
		}
	}
}
