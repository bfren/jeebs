// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities;

/// <summary>
/// PostMeta entity.
/// </summary>
public abstract record class WpPostMetaEntity : WpPostMetaEntityWithId
{
	/// <summary>
	/// PostId.
	/// </summary>
	public StrongIds.WpPostId PostId { get; init; } = new();

	/// <summary>
	/// Key.
	/// </summary>
	public string? Key { get; init; }

	/// <summary>
	/// Value.
	/// </summary>
	public string? Value { get; init; }
}
