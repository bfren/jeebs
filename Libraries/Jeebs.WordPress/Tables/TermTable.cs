using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Term Table
	/// </summary>
	public sealed class TermTable : Table
	{
		/// <summary>
		/// TermId
		/// </summary>
		public string TermId { get; } = "term_id";

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; } = "name";

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; } = "slug";

		/// <summary>
		/// Group
		/// </summary>
		public string Group { get; } = "term_group";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public TermTable(string prefix) : base($"{prefix}terms") { }
	}
}
