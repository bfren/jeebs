// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// UserMeta entity
	/// </summary>
	public abstract record WpUserMetaEntity : IWithId<WpUserMetaId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpUserMetaId Id
		{
			get =>
				new(UserMetaId);

			init =>
				UserMetaId = value.Value;
		}

		/// <summary>
		/// UserMetaId
		/// </summary>
		[Id]
		public long UserMetaId { get; init; }

		/// <summary>
		/// UserId
		/// </summary>
		public long UserId { get; init; }

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
