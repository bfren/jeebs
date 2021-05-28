// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="IContentFilter"/>
	public abstract class ContentFilter : IContentFilter
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

		/// <inheritdoc/>
		public string Execute(string content) =>
			filter(content);
	}
}
