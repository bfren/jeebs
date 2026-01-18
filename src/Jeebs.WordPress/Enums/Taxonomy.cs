// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;

namespace Jeebs.WordPress.Enums;

/// <summary>
/// Taxonomy enumeration.
/// </summary>
public sealed record class Taxonomy : Enumerated
{
	/// <summary>
	/// Create new value
	/// </summary>
	/// <param name="name">Value name</param>
	public Taxonomy(string name) : base(name) { }

	#region Default Taxonomies

	/// <summary>
	/// Blank
	/// </summary>
	public static readonly Taxonomy Blank = new(string.Empty);

	/// <summary>
	/// Category
	/// </summary>
	public static readonly Taxonomy PostCategory = new("category");

	/// <summary>
	/// Tag
	/// </summary>
	public static readonly Taxonomy PostTag = new("post_tag");

	/// <summary>
	/// Link
	/// </summary>
	public static readonly Taxonomy LinkCategory = new("link_category");

	/// <summary>
	/// Menu Item
	/// </summary>
	public static readonly Taxonomy NavMenu = new("nav_menu");

	#endregion Default Taxonomies

	/// <summary>
	/// List of all taxonomies
	/// Must be static so it is thread safe
	/// </summary>
	private static HashSet<Taxonomy> All { get; }

	internal static HashSet<Taxonomy> AllTest() =>
		All;

	/// <summary>
	/// Populate list of taxonomies
	/// </summary>
	static Taxonomy() =>
		All = new HashSet<Taxonomy>([PostCategory, PostTag, LinkCategory, NavMenu]);

	/// <summary>
	/// Add a custom taxonomy
	/// </summary>
	/// <param name="taxonomy">Taxonomy to add</param>
	public static bool AddCustomTaxonomy(Taxonomy taxonomy) =>
		All.Add(taxonomy);

	/// <summary>
	/// Parse Taxonomy value name
	/// </summary>
	/// <param name="name">Value name</param>
	public static Taxonomy Parse(string name) =>
		Parse(name, All.ToArray()).Unwrap(() => Blank);

	/// <summary>
	/// Returns whether or not the specified taxonomy has been registered
	/// </summary>
	/// <param name="taxonomy">Taxonomy to check</param>
	public static bool IsRegistered(Taxonomy taxonomy) =>
		IsRegistered(taxonomy.ToString(), All.ToArray());
}
