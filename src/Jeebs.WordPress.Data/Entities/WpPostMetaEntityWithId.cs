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
		[Ignore]
		public WpPostMetaId Id
		{
			get =>
				new(PostMetaId);

			init =>
				PostMetaId = value.Value;
		}

		/// <summary>
		/// PostMetaId
		/// </summary>
		[Id]
		public ulong PostMetaId { get; init; }
	}
}
