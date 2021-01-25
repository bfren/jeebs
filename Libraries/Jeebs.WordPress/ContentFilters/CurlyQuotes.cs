using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress.ContentFilters
{
	/// <summary>
	/// Curly Quotes
	/// </summary>
	public sealed class CurlyQuotes : ContentFilter
	{
		/// <inheritdoc/>
		private CurlyQuotes(Func<string, string> filter) : base(filter) { }

		/// <summary>
		/// Create filter
		/// </summary>
		public static ContentFilter Create() =>
			new CurlyQuotes(content => content.ConvertInnerHtmlQuotes());
	}
}
