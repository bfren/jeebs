// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// PostMeta entity
	/// </summary>
	public abstract record WpPostMetaEntityWithId : IWithId<WpPostMetaId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Id]
		public WpPostMetaId Id { get; init; } = new();
	}
}
