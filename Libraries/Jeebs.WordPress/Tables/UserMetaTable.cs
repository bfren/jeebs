using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// User Table
	/// </summary>
	public sealed class UserMetaTable : Table
	{
		/// <summary>
		/// UserMetaId
		/// </summary>
		public string UserMetaId { get; } = "umeta_id";

		/// <summary>
		/// UserId
		/// </summary>
		public string UserId { get; } = "user_id";

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
		public UserMetaTable(string prefix) : base($"{prefix}usermeta") { }
	}
}
