﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Post entity with ID properties
	/// </summary>
	public abstract record class WpPostEntityWithId : IWithId<WpPostId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Id]
		public WpPostId Id { get; init; } = new();
	}
}
