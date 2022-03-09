// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities;

/// <summary>
/// WordPress Comment Meta ID
/// </summary>
/// <param name="Value">ID Value</param>
public sealed record class WpCommentMetaId(long Value) : Id.IStrongId
{
	/// <summary>
	/// Define parameterless constructor for MVC model binding
	/// </summary>
	public WpCommentMetaId() : this(0) { }
}
