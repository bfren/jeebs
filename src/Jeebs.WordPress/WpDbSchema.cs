// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Tables;

namespace Jeebs.WordPress;

/// <inheritdoc cref="IWpDbSchema"/>
public sealed class WpDbSchema : IWpDbSchema
{
	/// <inheritdoc/>
	public CommentTable Comment { get; private init; }

	/// <inheritdoc/>
	public CommentMetaTable CommentMeta { get; private init; }

	/// <inheritdoc/>
	public LinkTable Link { get; private init; }

	/// <inheritdoc/>
	public OptionTable Opt { get; private init; }

	/// <inheritdoc/>
	public PostTable Post { get; private init; }

	/// <inheritdoc/>
	public PostMetaTable PostMeta { get; private init; }

	/// <inheritdoc/>
	public TermTable Term { get; private init; }

	/// <inheritdoc/>
	public TermMetaTable TermMeta { get; private init; }

	/// <inheritdoc/>
	public TermRelationshipTable TermRelationship { get; private init; }

	/// <inheritdoc/>
	public TermTaxonomyTable TermTaxonomy { get; private init; }

	/// <inheritdoc/>
	public UserTable User { get; private init; }

	/// <inheritdoc/>
	public UserMetaTable UserMeta { get; private init; }

	/// <summary>
	/// Create table objects
	/// </summary>
	/// <param name="prefix">Table prefix</param>
	public WpDbSchema(string prefix)
	{
		Comment = new CommentTable(prefix);
		CommentMeta = new CommentMetaTable(prefix);
		Link = new LinkTable(prefix);
		Opt = new OptionTable(prefix);
		Post = new PostTable(prefix);
		PostMeta = new PostMetaTable(prefix);
		Term = new TermTable(prefix);
		TermMeta = new TermMetaTable(prefix);
		TermRelationship = new TermRelationshipTable(prefix);
		TermTaxonomy = new TermTaxonomyTable(prefix);
		User = new UserTable(prefix);
		UserMeta = new UserMetaTable(prefix);
	}
}
