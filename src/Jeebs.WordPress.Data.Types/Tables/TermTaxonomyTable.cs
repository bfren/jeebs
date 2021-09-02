// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Data.Tables
{
	/// <summary>
	/// Term Taxonomy Table
	/// </summary>
	public sealed record class TermTaxonomyTable : Table
	{
		/// <summary>
		/// TermTaxonomyId
		/// </summary>
		public string Id =>
			"term_taxonomy_id";

		/// <summary>
		/// TermId
		/// </summary>
		public string TermId =>
			"term_id";

		/// <summary>
		/// Taxonomy
		/// </summary>
		public string Taxonomy =>
			"taxonomy";

		/// <summary>
		/// Description
		/// </summary>
		public string Description =>
			"description";

		/// <summary>
		/// ParentId
		/// </summary>
		public string ParentId =>
			"parent";

		/// <summary>
		/// Count
		/// </summary>
		public string Count =>
			"count";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public TermTaxonomyTable(string prefix) : base($"{prefix}term_taxonomy") { }
	}
}
