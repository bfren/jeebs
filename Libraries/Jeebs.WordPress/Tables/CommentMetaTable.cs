using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Comment Meta Table
	/// </summary>
	public sealed class CommentMetaTable : Table
	{
		/// <summary>
		/// CommentMetaId
		/// </summary>
		public readonly string CommentMetaId = "meta_id";

		/// <summary>
		/// CommentId
		/// </summary>
		public readonly string CommentId = "comment_id";

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
		/// <param name="prefix">Table Prefix</param>
		public CommentMetaTable(string prefix) : base($"{prefix}commentmeta") { }
	}
}
