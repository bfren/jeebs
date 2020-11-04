using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Term Taxonomy Table
	/// </summary>
	public sealed class TermTaxonomyTable : Table
	{
		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public string TermTaxonomyId { get; } = "term_taxonomy_id";

		/// <summary>
		/// TermId
		/// </summary>
		public string TermId { get; } = "term_id";

		/// <summary>
		/// Taxonomy
		/// </summary>
		public string Taxonomy { get; } = "taxonomy";

		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; } = "description";

		/// <summary>
		/// ParentId
		/// </summary>
		public string ParentId { get; } = "parent";

		/// <summary>
		/// Count
		/// </summary>
		public string Count { get; } = "count";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public TermTaxonomyTable(string prefix) : base($"{prefix}term_taxonomy") { }
	}
}
