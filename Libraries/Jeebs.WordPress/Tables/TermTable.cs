using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

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
		public readonly string TermId = "term_id";

		/// <summary>
		/// Title
		/// </summary>
		public readonly string Title = "name";

		/// <summary>
		/// Slug
		/// </summary>
		public readonly string Slug = "slug";

		/// <summary>
		/// Group
		/// </summary>
		public readonly string Group = "term_group";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public TermTable(in string prefix) : base($"{prefix}terms") { }
	}
}
