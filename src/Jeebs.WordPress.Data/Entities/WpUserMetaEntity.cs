// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// UserMeta entity
	/// </summary>
	public abstract record WpUserMetaEntity : WpUserMetaEntityWithId
	{
		/// <summary>
		/// UserId
		/// </summary>
		public WpUserId UserId { get; init; } = new();

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
