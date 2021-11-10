// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Data.Entities;

/// <summary>
/// WordPress Link ID
/// </summary>
/// <param name="Value">ID Value</param>
public readonly record struct WpLinkId(long Value) : IStrongId
{
	/// <summary>
	/// Define parameterless constructor for MVC model binding
	/// </summary>
	public WpLinkId() : this(0) { }
}
