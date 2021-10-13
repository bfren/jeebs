﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities;

/// <summary>
/// Option entity with ID properties
/// </summary>
public abstract record class WpOptionEntityWithId : IWithId<WpOptionId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpOptionId Id { get; init; } = new();
}
