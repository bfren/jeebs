// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Mapping;

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
		public string UserMetaId =>
			"umeta_id";

		/// <summary>
		/// UserId
		/// </summary>
		public string UserId =>
			"user_id";

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
		public UserMetaTable(string prefix) : base($"{prefix}usermeta") { }
	}
}
