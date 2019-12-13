using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Term Taxonomy Table
	/// </summary>
	public sealed class TermTaxonomyTable<T> : Table<T>
		where T : WpTermTaxonomyEntity
	{
		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public readonly string TermTaxonomyId = "term_taxonomy_id";

		/// <summary>
		/// TermId
		/// </summary>
		public readonly string TermId = "term_id";

		/// <summary>
		/// Taxonomy
		/// </summary>
		public readonly string Taxonomy = "taxonomy";

		/// <summary>
		/// Description
		/// </summary>
		public readonly string Description = "description";

		/// <summary>
		/// ParentId
		/// </summary>
		public readonly string ParentId = "parent";

		/// <summary>
		/// Count
		/// </summary>
		public readonly string Count = "count";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		/// <param name="prefix">Table prefix</param>
		public TermTaxonomyTable(in IAdapter adapter, in string prefix) : base(adapter, $"{prefix}term_taxonomy") { }
	}
}
