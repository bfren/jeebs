// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Tables;

namespace Jeebs.WordPress;

/// <summary>
/// WordPress Database schema
/// </summary>
public interface IWpDbSchema
{
	/// <summary>
	/// Comment Table
	/// </summary>
	CommentsTable Comments { get; }

	/// <summary>
	/// Comment Meta Table
	/// </summary>
	CommentsMetaTable CommentsMeta { get; }

	/// <summary>
	/// Link Table
	/// </summary>
	LinksTable Links { get; }

	/// <summary>
	/// Option Table
	/// </summary>
	OptionsTable Options { get; }

	/// <summary>
	/// Post Table
	/// </summary>
	PostsTable Posts { get; }

	/// <summary>
	/// Post Meta Table
	/// </summary>
	PostsMetaTable PostsMeta { get; }

	/// <summary>
	/// Term Table
	/// </summary>
	TermsTable Terms { get; }

	/// <summary>
	/// Term Meta Table
	/// </summary>
	TermsMetaTable TermsMeta { get; }

	/// <summary>
	/// Term Relationship Table
	/// </summary>
	TermRelationshipsTable TermRelationships { get; }

	/// <summary>
	/// Term Taxonomy Table
	/// </summary>
	TermTaxonomiesTable TermTaxonomies { get; }

	/// <summary>
	/// User Table
	/// </summary>
	UsersTable Users { get; }

	/// <summary>
	/// User Meta Table
	/// </summary>
	UsersMetaTable UsersMeta { get; }
}
