// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// PostMeta entity
	/// </summary>
	public abstract record WpPostMetaEntity : IWithId<WpPostMetaId>
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
		public long PostMetaId { get; init; }

		/// <summary>
		/// PostId
		/// </summary>
		public long PostId { get; init; }

		/// <summary>
		/// Key
		/// </summary>
		public string? Key { get; init; }

		/// <summary>
		/// Value
		/// </summary>
		public string? Value { get; init; }
	}
}
