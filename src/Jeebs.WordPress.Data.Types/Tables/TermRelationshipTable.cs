﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Data.Tables
{
	/// <summary>
	/// Term Relationship Table
	/// </summary>
	public sealed record TermRelationshipTable : Table
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
