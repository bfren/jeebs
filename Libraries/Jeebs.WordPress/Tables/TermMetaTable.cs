using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Term Meta Table
	/// </summary>
	public sealed class TermMetaTable : Table
	{
		/// <summary>
		/// TermMetaId
		/// </summary>
		public readonly string TermMetaId = "meta_id";

		/// <summary>
		/// TermId
		/// </summary>
		public readonly string TermId = "term_id";

		/// <summary>
		/// Key
		/// </summary>
		public readonly string Key = "meta_key";

		/// <summary>
		/// Value
		/// </summary>
		public readonly string Value = "meta_value";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public TermMetaTable(string prefix) : base($"{prefix}termmeta") { }
	}
}
