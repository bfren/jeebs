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
	protected override Maybe<QueryParts> Build(Maybe<QueryParts> parts) =>
		base.Build(
			parts
		)
		.SwitchIf(
			_ => PostId?.Value > 0 || PostIds.Count > 0,
			ifTrue: x => Builder.AddWherePostId(x, PostId, PostIds)
		)
		.SwitchIf(
			_ => string.IsNullOrEmpty(Key),
			ifFalse: x => Builder.AddWhereKey(x, Key)
		);
}
