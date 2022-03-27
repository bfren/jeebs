// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities;

/// <summary>
/// CommentMeta entity
/// </summary>
public abstract record class WpCommentMetaEntity : WpCommentMetaEntityWithId
{
	/// <summary>
	/// CommentId
	/// </summary>
	public StrongIds.WpCommentId CommentId { get; init; }

	/// <summary>
	/// Key
	/// </summary>
	public string? Key { get; init; }

	/// <summary>
	/// Value
	/// </summary>
	public string? Value { get; init; }
}
