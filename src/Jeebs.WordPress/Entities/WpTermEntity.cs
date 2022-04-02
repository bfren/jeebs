// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Term entity
/// </summary>
public abstract record class WpTermEntity : WpTermEntityWithId
{
	/// <summary>
	/// Title
	/// </summary>
	public string Title { get; init; } = string.Empty;

	/// <summary>
	/// Slug
	/// </summary>
	public string Slug { get; init; } = string.Empty;

	/// <summary>
	/// Group
	/// </summary>
	public ulong Group { get; init; }
}
