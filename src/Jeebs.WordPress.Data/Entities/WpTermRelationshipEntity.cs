// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// TermRelationship entity
	/// </summary>
	public abstract record WpTermRelationshipEntity : IWithId<WpTermRelationshipId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpTermRelationshipId Id
		{
			get =>
				new(PostId);

			init =>
				PostId = value.Value;
		}

		/// <summary>
		/// PostId
		/// </summary>
		[Id]
		public long PostId { get; init; }

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public long TermTaxonomyId { get; init; }

		/// <summary>
		/// SortOrder
		/// </summary>
		public long SortOrder { get; init; }
	}
}
