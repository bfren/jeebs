﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Data.Entities;

/// <summary>
/// WordPress Post Meta ID
/// </summary>
/// <param name="Value">ID Value</param>
public sealed record class WpPostMetaId(long Value) : IStrongId
{
	/// <summary>
	/// Define parameterless constructor for MVC model binding
	/// </summary>
	public WpPostMetaId() : this(0) { }
}
