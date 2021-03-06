// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Content filter
	/// </summary>
	public abstract class ContentFilter
	{
		/// <summary>
		/// Content filter function
		/// </summary>
		private readonly Func<string, string> filter;

		/// <summary>
		/// Setup object
		/// </summary>
		/// <param name="filter">Content filter function</param>
		protected ContentFilter(Func<string, string> filter) =>
			this.filter = filter;

		/// <summary>
		/// Execute filter
		/// </summary>
		/// <param name="content">Content</param>
		/// <returns>Filtered content</returns>
		public string Execute(string content) =>
			filter(content);
	}
}
