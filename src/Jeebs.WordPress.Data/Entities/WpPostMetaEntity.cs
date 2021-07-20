// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// PostMeta entity
	/// </summary>
	public abstract record WpPostMetaEntity : WpPostMetaEntityWithId
	{
		/// <summary>
		/// PostId
		/// </summary>
		public WpPostId PostId { get; init; } = new();

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
