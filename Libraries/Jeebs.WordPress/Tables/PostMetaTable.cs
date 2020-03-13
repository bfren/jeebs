using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Post Meta Table
	/// </summary>
	public sealed class PostMetaTable : Table
	{
		/// <summary>
		/// PostMetaId
		/// </summary>
		public readonly string PostMetaId = "meta_id";

		/// <summary>
		/// PostId
		/// </summary>
		public readonly string PostId = "post_id";

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
		public PostMetaTable(string prefix) : base($"{prefix}postmeta") { }
	}
}
