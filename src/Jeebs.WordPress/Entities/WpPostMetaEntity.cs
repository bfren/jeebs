// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// PostMeta entity
	/// </summary>
	public abstract record WpPostMetaEntity : IEntity, IEntity<long>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		long IEntity.Id =>
			Id.Value;

		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public IStrongId<long> Id
		{
			get =>
				new WpPostMetaId(PostMetaId);

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
		public string Key { get; init; } = string.Empty;

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; init; } = string.Empty;
	}
}
