// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Data.Tables
{
	/// <summary>
	/// User Table
	/// </summary>
	public sealed record UserMetaTable : Table
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
