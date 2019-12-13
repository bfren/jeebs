using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Term Relationship Table
	/// </summary>
	public sealed class TermRelationshipTable<T> : Table<T>
		where T : WpTermRelationshipEntity
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
		/// <param name="adapter">IAdapter</param>
		/// <param name="prefix">Table prefix</param>
		public TermRelationshipTable(in IAdapter adapter, in string prefix) : base(adapter, $"{prefix}term_relationships") { }
	}
}
