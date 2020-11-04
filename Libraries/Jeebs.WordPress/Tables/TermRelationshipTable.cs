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
		public string PostId { get; } = "object_id";

		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public string TermTaxonomyId { get; } = "term_taxonomy_id";

		/// <summary>
		/// SortOrder
		/// </summary>
		public string SortOrder { get; } = "term_order";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public TermRelationshipTable(string prefix) : base($"{prefix}term_relationships") { }
	}
}
