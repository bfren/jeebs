// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;

namespace Jeebs.WordPress.Data.Enums;

/// <summary>
/// PostType enumeration
/// </summary>
public sealed record class PostType : Enumerated
{
	/// <summary>
	/// Create new value
	/// </summary>
	/// <param name="name">Value name</param>
	public PostType(string name) : base(name) { }

	#region Default Post Types

	/// <summary>
	/// Post
	/// </summary>
	public static readonly PostType Post = new("post");

	/// <summary>
	/// Page
	/// </summary>
	public static readonly PostType Page = new("page");

	/// <summary>
	/// Revision
	/// </summary>
	public static readonly PostType Revision = new("revision");

	/// <summary>
	/// Attachment
	/// </summary>
	public static readonly PostType Attachment = new("attachment");

	/// <summary>
	/// Menu Item
	/// </summary>
	public static readonly PostType MenuItem = new("nav_menu_item");

	/// <summary>
	/// Advanced Custom Field
	/// </summary>
	public static readonly PostType AdvancedCustomField = new("acf");

	#endregion Default Post Types

	/// <summary>
	/// List of all post types
	/// Must be static so it is thread safe
	/// </summary>
	private static readonly HashSet<PostType> all;

	internal static HashSet<PostType> AllTest() =>
		all;

	/// <summary>
	/// Populate set of post types
	/// </summary>
	static PostType() =>
		all = new HashSet<PostType>(new[] { Post, Page, Revision, Attachment, MenuItem, AdvancedCustomField });

	/// <summary>
	/// Add a custom post type
	/// </summary>
	/// <param name="type">PostType to add</param>
	public static bool AddCustomPostType(PostType type) =>
		all.Add(type);

	/// <summary>
	/// Parse PostType value name
	/// </summary>
	/// <param name="name">Value name</param>
	public static PostType Parse(string name) =>
		Parse(name, all.ToArray()).Unwrap(() => Post);
}
