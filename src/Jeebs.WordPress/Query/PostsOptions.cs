// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.QueryBuilder;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="Options.IQueryPostsOptions"/>
public sealed record class PostsOptions : Options.PostsOptions
{
	/// <summary>
	/// Internal creation only.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	internal PostsOptions(IWpDbSchema schema) : base(schema, new PostsPartsBuilder(schema)) { }

	/// <summary>
	/// Allow Builder to be injected.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	/// <param name="builder">IQueryPostsPartsBuilder.</param>
	internal PostsOptions(IWpDbSchema schema, IQueryPostsPartsBuilder builder) : base(schema, builder) { }

	/// <inheritdoc/>
	protected override Result<QueryParts> Build(Result<QueryParts> parts) =>
		base.Build(
			parts
		)
		.Bind(
			x => Builder.AddWhereType(x, Type)
		)
		.Bind(
			x => Builder.AddWhereStatus(x, Status)
		)
		.If(
			_ => !string.IsNullOrEmpty(SearchText),
			x => Builder.AddWhereSearch(x, SearchFields, SearchComparison, SearchText)
		)
		.If(
			_ => FromDate is not null,
			x => Builder.AddWherePublishedFrom(x, FromDate)
		)
		.If(
			_ => ToDate is not null,
			x => Builder.AddWherePublishedTo(x, ToDate)
		)
		.If(
			_ => ParentId is not null,
			x => Builder.AddWhereParentId(x, ParentId)
		)
		.If(
			_ => Taxonomies.Count > 0,
			x => Builder.AddWhereTaxonomies(x, Taxonomies)
		)
		.If(
			_ => CustomFields.Count > 0,
			x => Builder.AddWhereCustomFields(x, CustomFields)
		);
}
