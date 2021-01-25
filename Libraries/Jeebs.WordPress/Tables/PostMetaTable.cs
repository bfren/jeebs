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
		public string PostMetaId =>
			"meta_id";

		/// <summary>
		/// PostId
		/// </summary>
		public string PostId =>
			"post_id";

		/// <summary>
		/// Key
		/// </summary>
		public string Key =>
			"meta_key";

		/// <summary>
		/// Value
		/// </summary>
		public string Value =>
			"meta_value";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public PostMetaTable(string prefix) : base($"{prefix}postmeta") { }
	}
}
