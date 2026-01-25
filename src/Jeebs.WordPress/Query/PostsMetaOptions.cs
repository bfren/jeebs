// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="Options.IQueryPostsMetaOptions"/>
public sealed record class PostsMetaOptions : Options.PostsMetaOptions
{
	/// <summary>
	/// Internal creation only.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	internal PostsMetaOptions(IWpDbSchema schema) : this(schema, new PostsMetaPartsBuilder(schema)) { }

	/// <summary>
	/// Allow Builder to be injected.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	/// <param name="builder">IQueryPostsMetaPartsBuilder.</param>
	internal PostsMetaOptions(IWpDbSchema schema, IQueryPostsMetaPartsBuilder builder) : base(schema, builder) =>
		Maximum = null;

	/// <inheritdoc/>
	protected override Result<QueryParts> Build(Result<QueryParts> parts) =>
		base.Build(
			parts
		)
		.If(
			_ => PostId?.Value > 0 || PostIds.Count > 0,
			x => Builder.AddWherePostId(x, PostId, PostIds)
		)
		.If(
			_ => !string.IsNullOrEmpty(Key),
			x => Builder.AddWhereKey(x, Key)
		);
}
