using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// User Table
	/// </summary>
	public sealed class UserMetaTable<T> : Table<T>
		where T : WpUserMetaEntity
	{
		/// <summary>
		/// UserMetaId
		/// </summary>
		public readonly string UserMetaId = "umeta_id";

		/// <summary>
		/// UserId
		/// </summary>
		public readonly string UserId = "user_id";

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
		public UserMetaTable(in IAdapter adapter, in string prefix) : base(adapter, $"{prefix}usermeta") { }
	}
}
