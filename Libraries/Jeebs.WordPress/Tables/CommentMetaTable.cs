using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

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
		public string CommentMetaId { get; } = "meta_id";

		/// <summary>
		/// CommentId
		/// </summary>
		public string CommentId { get; } = "comment_id";

		/// <summary>
		/// Key
		/// </summary>
		public string Key { get; } = "meta_key";

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; } = "meta_value";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table Prefix</param>
		public CommentMetaTable(string prefix) : base($"{prefix}commentmeta") { }
	}
}
