// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Enums;

/// <summary>
/// PostStatus enumeration.
/// </summary>
public sealed record class PostStatus : Enumerated
{
	/// <summary>
	/// Create new value
	/// </summary>
	/// <param name="name">Value name</param>
	public PostStatus(string name) : base(name) { }

	#region Default Post Statuses

	/// <summary>
	/// Published post
	/// </summary>
	public static readonly PostStatus Publish = new("publish");

	/// <summary>
	/// Inherit
	/// </summary>
	public static readonly PostStatus Inherit = new("inherit");

	/// <summary>
	/// Pending
	/// </summary>
	public static readonly PostStatus Pending = new("pending");

	/// <summary>
	/// Draft
	/// </summary>
	public static readonly PostStatus Draft = new("draft");

	/// <summary>
	/// Auto Draft
	/// </summary>
	public static readonly PostStatus AutoDraft = new("auto-draft");

	#endregion Default Post Statuses

	/// <summary>
	/// Parse PostStatus value name
	/// </summary>
	/// <param name="name">Value name</param>
	public static PostStatus Parse(string name) =>
		Parse(name, values: [Publish, Inherit, Pending, Draft, AutoDraft]).Unwrap(() => Draft);
}
