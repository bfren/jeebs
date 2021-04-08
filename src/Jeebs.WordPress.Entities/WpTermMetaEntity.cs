// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// TermMeta entity
	/// </summary>
	public abstract record WpTermMetaEntity : IWithId<WpTermMetaId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpTermMetaId Id
		{
			get =>
				new(TermMetaId);

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
