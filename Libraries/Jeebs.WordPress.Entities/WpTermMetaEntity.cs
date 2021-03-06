// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermMeta entity
	/// </summary>
	public abstract record WpTermMetaEntity : IEntity, IEntity<long>
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
				new WpTermMetaId(TermMetaId);

			init =>
				TermMetaId = value.Value;
		}

		/// <summary>
		/// TermMetaId
		/// </summary>
		[Id]
		public long TermMetaId { get; init; }

		/// <summary>
		/// TermId
		/// </summary>
		public long TermId { get; init; }

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
