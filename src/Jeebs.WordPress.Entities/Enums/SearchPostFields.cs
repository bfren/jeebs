// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.WordPress.Enums
{
	/// <summary>
	/// Search Post Fields
	/// </summary>
	[Flags]
	public enum SearchPostFields
	{
		/// <summary>
		/// Search nothing
		/// </summary>
		None = 0,

		/// <summary>
		/// Search Title field
		/// </summary>
		Title = 1,

		/// <summary>
		/// Search Slug field
		/// </summary>
		Slug = 2,

		/// <summary>
		/// Search Content field
		/// </summary>
		Content = 4,

		/// <summary>
		/// Search Excerpt field
		/// </summary>
		Excerpt = 8,

		/// <summary>
		/// Search All fields
		/// </summary>
		All = Title | Slug | Content | Excerpt
	}
}
