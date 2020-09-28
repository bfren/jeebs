using System;
using System.Collections.Generic;
using System.Text;
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
		public readonly string PostId = "object_id";

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public readonly string TermTaxonomyId = "term_taxonomy_id";

		/// <summary>
		/// SortOrder
		/// </summary>
		public readonly string SortOrder = "term_order";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public TermRelationshipTable(string prefix) : base($"{prefix}term_relationships") { }
	}
}
