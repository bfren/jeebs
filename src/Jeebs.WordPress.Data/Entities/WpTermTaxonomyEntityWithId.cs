// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermTaxonomy entity
	/// </summary>
	public abstract record WpTermTaxonomyEntityWithId : IWithId<WpTermTaxonomyId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpTermTaxonomyId Id
		{
			get =>
				new(TermTaxonomyId);

			init =>
				TermTaxonomyId = value.Value;
		}

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		[Id]
		public long TermTaxonomyId { get; init; }
	}
}
