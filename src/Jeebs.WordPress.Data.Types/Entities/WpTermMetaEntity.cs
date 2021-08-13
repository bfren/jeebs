// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermMeta entity
	/// </summary>
	public abstract record class WpTermMetaEntity : WpTermMetaEntityWithId
	{
		/// <summary>
		/// TermId
		/// </summary>
		public WpTermId TermId { get; init; } = new();

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
