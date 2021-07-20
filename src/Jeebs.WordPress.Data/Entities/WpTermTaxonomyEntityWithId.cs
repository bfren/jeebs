﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermTaxonomy entity
	/// </summary>
	public abstract record WpTermTaxonomyEntityWithId : IWithId<WpTermTaxonomyId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Id]
		public WpTermTaxonomyId Id { get; init; } = new();
	}
}
