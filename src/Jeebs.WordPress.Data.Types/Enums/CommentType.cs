// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Data.Enums;

/// <summary>
/// CommentType enumeration
/// </summary>
public sealed class CommentType : Enumerated
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

	#endregion

	/// <summary>
	/// Parse CommentType value name
	/// </summary>
	/// <param name="name">Value name</param>
	/// <returns>CommentType object</returns>
	public static CommentType Parse(string name) =>
		Parse(name, values: new[] { Blank, Pingback }).Unwrap(() => Blank);
}
