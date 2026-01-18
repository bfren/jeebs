// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Tables;

namespace Jeebs.WordPress;

/// <inheritdoc cref="IWpDbSchema"/>
public sealed class WpDbSchema : IWpDbSchema
{
	/// <inheritdoc/>
	public CommentsTable Comments { get; private init; }

	/// <inheritdoc/>
	public CommentsMetaTable CommentsMeta { get; private init; }

	/// <inheritdoc/>
	public LinksTable Links { get; private init; }

	/// <inheritdoc/>
	public OptionsTable Options { get; private init; }

	/// <inheritdoc/>
	public PostsTable Posts { get; private init; }

	/// <inheritdoc/>
	public PostsMetaTable PostsMeta { get; private init; }

	/// <inheritdoc/>
	public TermsTable Terms { get; private init; }

	/// <inheritdoc/>
	public TermsMetaTable TermsMeta { get; private init; }

	/// <inheritdoc/>
	public TermRelationshipsTable TermRelationships { get; private init; }

	/// <inheritdoc/>
	public TermTaxonomiesTable TermTaxonomies { get; private init; }

	/// <inheritdoc/>
	public UsersTable Users { get; private init; }

	/// <inheritdoc/>
	public UsersMetaTable UsersMeta { get; private init; }

	/// <summary>
	/// Create table objects.
	/// </summary>
	/// <param name="prefix">Table prefix</param>
	public WpDbSchema(string prefix)
	{
		Comments = new CommentsTable(prefix);
		CommentsMeta = new CommentsMetaTable(prefix);
		Links = new LinksTable(prefix);
		Options = new OptionsTable(prefix);
		Posts = new PostsTable(prefix);
		PostsMeta = new PostsMetaTable(prefix);
		Terms = new TermsTable(prefix);
		TermsMeta = new TermsMetaTable(prefix);
		TermRelationships = new TermRelationshipsTable(prefix);
		TermTaxonomies = new TermTaxonomiesTable(prefix);
		Users = new UsersTable(prefix);
		UsersMeta = new UsersMetaTable(prefix);
	}
}
