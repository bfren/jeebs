// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Option entity
	/// </summary>
	public abstract record WpOptionEntity : IEntity, IEntity<long>
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
				new WpOptionId(OptionId);

			init =>
				OptionId = value.Value;
		}

		/// <summary>
		/// OptionId
		/// </summary>
		[Id]
		public long OptionId { get; init; }

		/// <summary>
		/// Key
		/// </summary>
		public string Key { get; init; } = string.Empty;

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; init; } = string.Empty;

		/// <summary>
		/// IsAutoloaded
		/// </summary>
		public bool IsAutoloaded { get; init; }
	}
}
