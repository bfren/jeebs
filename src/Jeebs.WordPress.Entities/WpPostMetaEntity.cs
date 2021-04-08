// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Entities
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
		public string Key { get; init; } = string.Empty;

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; init; } = string.Empty;
	}
}
