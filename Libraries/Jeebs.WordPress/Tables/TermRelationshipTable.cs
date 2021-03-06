// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Term Relationship Table
	/// </summary>
	public sealed class TermRelationshipTable : Table
	{
		/// <summary>
		/// PostId
		/// </summary>
		public string PostId =>
			"object_id";

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public string TermTaxonomyId =>
			"term_taxonomy_id";

		/// <summary>
		/// SortOrder
		/// </summary>
		public string SortOrder =>
			"term_order";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public TermRelationshipTable(string prefix) : base($"{prefix}term_relationships") { }
	}
}
