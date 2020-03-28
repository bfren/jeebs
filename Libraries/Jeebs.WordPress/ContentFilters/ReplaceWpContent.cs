using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress.ContentFilters
{
	/// <summary>
	/// Replace wp-content/ URLs (e.g. with static file server)
	/// </summary>
	public sealed class ReplaceWpContent : ContentFilter
	{
		/// <summary>
		/// Use factory pattern
		/// </summary>
		/// <param name="filter">Content filter function</param>
		private ReplaceWpContent(Func<string, string> filter) : base(filter) { }

		/// <summary>
		/// Create filter
		/// </summary>
		/// <param name="from">Search URL</param>
		/// <param name="to">Replacement URL</param>
		/// <returns>IContentFilter</returns>
		public static ContentFilter Create(string from, string to) => new ReplaceWpContent(content => content.Replace(from, to));
	}
}
