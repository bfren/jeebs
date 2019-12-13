using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Term Meta Table
	/// </summary>
	public sealed class TermMetaTable<T> : Table<T>
		where T : WpTermMetaEntity
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
		/// <param name="adapter">IAdapter</param>
		/// <param name="prefix">Table prefix</param>
		public TermMetaTable(in IAdapter adapter, in string prefix) : base(adapter, $"{prefix}termmeta") { }
	}
}
