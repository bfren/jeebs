// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.WordPress.Enums;

/// <summary>
/// Search Post Fields
/// </summary>
[Flags]
public enum SearchPostField
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
	Slug = 1 << 1,

	/// <summary>
	/// Search Content field
	/// </summary>
	Content = 1 << 2,

	/// <summary>
	/// Search Excerpt field
	/// </summary>
	Excerpt = 1 << 3,

	/// <summary>
	/// Search All fields
	/// </summary>
	All = Title | Slug | Content | Excerpt
}
