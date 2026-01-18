// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Enums;

/// <summary>
/// CommentType enumeration.
/// </summary>
public sealed record class CommentType : Enumerated
{
	/// <summary>
	/// Create new value
	/// </summary>
	/// <param name="name">Value name</param>
	public CommentType(string name) : base(name) { }

	#region Default Comment Types

	/// <summary>
	/// Blank comment
	/// </summary>
	public static readonly CommentType Blank = new(string.Empty);

	/// <summary>
	/// Pingback comment
	/// </summary>
	public static readonly CommentType Pingback = new("pingback");

	#endregion Default Comment Types

	/// <summary>
	/// Parse CommentType value name
	/// </summary>
	/// <param name="name">Value name</param>
	public static CommentType Parse(string name) =>
		Parse(name, values: [Blank, Pingback]).Unwrap(() => Blank);
}
