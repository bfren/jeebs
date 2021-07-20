// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// UserMeta entity
	/// </summary>
	public abstract record WpUserMetaEntityWithId : IWithId<WpUserMetaId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Id]
		public WpUserMetaId Id { get; init; } = new();
	}
}
