// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.WordPress.Tables;

/// <summary>
/// User Table.
/// </summary>
public sealed record class UsersMetaTable : Table
{
	/// <summary>
	/// UserMetaId.
	/// </summary>
	[Id]
	public string Id =>
		"umeta_id";

	/// <summary>
	/// UserId.
	/// </summary>
	public string UserId =>
		"user_id";

	/// <summary>
	/// Key.
	/// </summary>
	public string Key =>
		"meta_key";

	/// <summary>
	/// Value.
	/// </summary>
	public string Value =>
		"meta_value";

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="prefix">Table prefix</param>
	public UsersMetaTable(string prefix) : base($"{prefix}usermeta") { }
}
