// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Data.Tables
{
	/// <summary>
	/// Term Table
	/// </summary>
	public sealed record TermTable : Table
	{
		/// <summary>
		/// TermId
		/// </summary>
		public string TermId =>
			"term_id";

		/// <summary>
		/// Title
		/// </summary>
		public string Title =>
			"name";

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug =>
			"slug";

		/// <summary>
		/// Group
		/// </summary>
		public string Group =>
			"term_group";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public TermTable(string prefix) : base($"{prefix}terms") { }
	}
}
