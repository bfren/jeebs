// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.WordPress.Data.ContentFilters
{
	/// <summary>
	/// Curly Quotes
	/// </summary>
	public sealed class CurlQuotes : ContentFilter
	{
		/// <inheritdoc/>
		private CurlQuotes(Func<string, string> filter) : base(filter) { }

		/// <summary>
		/// Create filter
		/// </summary>
		public static ContentFilter Create() =>
			new CurlQuotes(content => content.ConvertInnerHtmlQuotes());
	}
}
