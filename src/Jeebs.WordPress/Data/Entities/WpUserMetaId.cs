// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Data.Entities;

/// <summary>
/// WordPress User Meta ID
/// </summary>
/// <param name="Value">ID Value</param>
public sealed record class WpUserMetaId(long Value) : Id.IStrongId
{
	/// <summary>
	/// Define parameterless constructor for MVC model binding
	/// </summary>
	public WpUserMetaId() : this(0) { }
}
