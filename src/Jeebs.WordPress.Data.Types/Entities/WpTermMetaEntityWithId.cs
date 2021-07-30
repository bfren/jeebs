﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermMeta entity
	/// </summary>
	public abstract record WpTermMetaEntityWithId : IWithId<WpTermMetaId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Id]
		public WpTermMetaId Id { get; init; } = new();
	}
}
