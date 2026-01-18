// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities;

/// <summary>
/// UserMeta entity.
/// </summary>
public abstract record class WpUserMetaEntity : WpUserMetaEntityWithId
{
	/// <summary>
	/// UserId.
	/// </summary>
	public StrongIds.WpUserId UserId { get; init; } = new();

	/// <summary>
	/// Key.
	/// </summary>
	public string? Key { get; init; }

	/// <summary>
	/// Value.
	/// </summary>
	public string? Value { get; init; }
}
