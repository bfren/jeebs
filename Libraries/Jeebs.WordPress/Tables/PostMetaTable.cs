using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

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
		public string PostMetaId { get; } = "meta_id";

		/// <summary>
		/// PostId
		/// </summary>
		public string PostId { get; } = "post_id";

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
		/// <param name="prefix">Table prefix</param>
		public PostMetaTable(string prefix) : base($"{prefix}postmeta") { }
	}
}
