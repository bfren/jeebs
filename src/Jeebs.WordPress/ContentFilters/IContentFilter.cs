// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.ContentFilters;

/// <summary>
/// Content filter.
/// </summary>
public interface IContentFilter
{
	/// <summary>
	/// Execute filter.
	/// </summary>
	/// <param name="content">Original content</param>
	string Execute(string content);
}
