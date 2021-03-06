// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

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
