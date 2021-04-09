// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Term entity
	/// </summary>
	public abstract record WpTermEntity : IWithId<WpTermId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpTermId Id
		{
			get =>
				new(TermId);

			init =>
				TermId = value.Value;
		}

		/// <summary>
		/// TermId
		/// </summary>
		[Id]
		public long TermId { get; init; }

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; init; } = string.Empty;

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; init; } = string.Empty;

		/// <summary>
		/// Group
		/// </summary>
		public long Group { get; init; }
	}
}
