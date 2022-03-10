// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.WordPress.Tables;

/// <summary>
/// Link Table
/// </summary>
public sealed record class LinksTable : Table
{
	/// <summary>
	/// LinkId
	/// </summary>
	public string Id =>

		"link_id";

	/// <summary>
	/// Url
	/// </summary>
	public string Url =>

		"link_url";

	/// <summary>
	/// Title
	/// </summary>
	public string Title =>

		"link_name";

	/// <summary>
	/// Image
	/// </summary>
	public string Image =>
		"link_image";

	/// <summary>
	/// Target
	/// </summary>
	public string Target =>
		"link_target";

	/// <summary>
	/// CategoryId
	/// </summary>
	public string CategoryId =>
		"link_category";

	/// <summary>
	/// Description
	/// </summary>
	public string Description =>
		"link_description";

	/// <summary>
	/// Visible
	/// </summary>
	public string Visible =>
		"link_visible";

	/// <summary>
	/// OwnerId
	/// </summary>
	public string OwnerId =>
		"link_owner";

	/// <summary>
	/// Rating
	/// </summary>
	public string Rating =>
		"link_rating";

	/// <summary>
	/// LastUpdatedOn
	/// </summary>
	public string LastUpdatedOn =>
		"link_updated";

	/// <summary>
	/// Rel
	/// </summary>
	public string Rel =>
		"link_rel";

	/// <summary>
	/// Notes
	/// </summary>
	public string Notes =>
		"link_notes";

	/// <summary>
	/// Rss
	/// </summary>
	public string Rss =>
		"link_rss";

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="prefix">Table prefix</param>
	public LinksTable(string prefix) : base($"{prefix}links") { }
}
