// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Data.Tables;

/// <summary>
/// Comment Table
/// </summary>
public sealed record class CommentTable : Table
{
	/// <summary>
	/// CommentId
	/// </summary>
	public string Id =>
		"comment_ID";

	/// <summary>
	/// PostId
	/// </summary>
	public string PostId =>
		"comment_post_ID";

	/// <summary>
	/// AuthorName
	/// </summary>
	public string AuthorName =>
		"comment_author";

	/// <summary>
	/// AuthorEmail
	/// </summary>
	public string AuthorEmail =>
		"comment_author_email";

	/// <summary>
	/// AuthorUrl
	/// </summary>
	public string AuthorUrl =>
		"comment_author_url";

	/// <summary>
	/// AuthorIp
	/// </summary>
	public string AuthorIp =>
		"comment_author_IP";

	/// <summary>
	/// PublishedOn
	/// </summary>
	public string PublishedOn =>
		"comment_date";

	/// <summary>
	/// PublishedOnGmt
	/// </summary>
	public string PublishedOnGmt =>
		"comment_date_gmt";

	/// <summary>
	/// Content
	/// </summary>
	public string Content =>
		"comment_content";

	/// <summary>
	/// Karma
	/// </summary>
	public string Karma =>
		"comment_karma";

	/// <summary>
	/// IsApproved
	/// </summary>
	public string IsApproved =>
		"comment_approved";

	/// <summary>
	/// AuthorUserAgent
	/// </summary>
	public string AuthorUserAgent =>
		"comment_agent";

	/// <summary>
	/// Type
	/// </summary>
	public string Type =>
		"comment_type";

	/// <summary>
	/// ParentId
	/// </summary>
	public string ParentId =>
		"comment_parent";

	/// <summary>
	/// AuthorId
	/// </summary>
	public string AuthorId =>
		"user_id";

	/// <summary>
	/// AuthorIsSubscribed
	/// </summary>
	public string AuthorIsSubscribed =>
		"comment_subscribe";

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="prefix">Table prefix</param>
	public CommentTable(string prefix) : base($"{prefix}comments") { }
}
