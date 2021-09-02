// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Data.Entities;

/// <summary>
/// Option entity
/// </summary>
public abstract record class WpOptionEntity : WpOptionEntityWithId
{
	/// <summary>
	/// Key
	/// </summary>
	public string? Key { get; init; }

	/// <summary>
	/// Value
	/// </summary>
	public string Value { get; init; } = string.Empty;

	/// <summary>
	/// IsAutoloaded
	/// </summary>
	public bool IsAutoloaded { get; init; }
}
