// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.WordPress.Tables;

/// <summary>
/// Comment Meta Table
/// </summary>
public sealed record class CommentsMetaTable : Table
{
	/// <summary>
	/// CommentMetaId
	/// </summary>
	[Id]
	public string Id =>
		"meta_id";

	/// <summary>
	/// CommentId
	/// </summary>
	public string CommentId =>
		"comment_id";

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
	/// <param name="prefix">Table Prefix</param>
	public CommentsMetaTable(string prefix) : base($"{prefix}commentmeta") { }
}
