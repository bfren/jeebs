// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsOptions"/>
		public sealed record PostsOptions : Options<WpPostId>, IQueryPostsOptions
		{
			private new IQueryPostsPartsBuilder Builder =>
				(IQueryPostsPartsBuilder)base.Builder;

			/// <inheritdoc/>
			public PostType Type { get; init; } = PostType.Post;

			/// <inheritdoc/>
			public PostStatus Status { get; init; } = PostStatus.Publish;

			/// <inheritdoc/>
			public string? SearchText { get; init; }

			/// <inheritdoc/>
			public SearchPostFields SearchFields { get; init; } = SearchPostFields.All;

			/// <inheritdoc/>
			public Compare SearchComparison { get; init; } = Compare.Like;

			/// <inheritdoc/>
			public DateTime? From { get; init; }

			/// <inheritdoc/>
			public DateTime? To { get; init; }

			/// <inheritdoc/>
			public long? ParentId { get; init; }

			/// <inheritdoc/>
			public IImmutableList<(Taxonomy taxonomy, long id)> Taxonomies { get; init; } =
				new ImmutableList<(Taxonomy taxonomy, long id)>();

			/// <inheritdoc/>
			public IImmutableList<(ICustomField field, Compare cmp, object value)> CustomFields { get; init; } =
				new ImmutableList<(ICustomField field, Compare cmp, object value)>();

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			internal PostsOptions(IWpDbSchema schema) : base(schema, new PostsPartsBuilder(schema)) { }

			/// <summary>
			/// Allow Builder to be injected
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			/// <param name="builder">IQueryPostsPartsBuilder</param>
			internal PostsOptions(IWpDbSchema schema, IQueryPostsPartsBuilder builder) : base(schema, builder) { }

			/// <inheritdoc/>
			protected override Option<QueryParts> Build(Option<QueryParts> parts) =>
				base.Build(
					parts
				)
				.Bind(
					x => Builder.AddWhereType(x, Type)
				)
				.Bind(
					x => Builder.AddWhereStatus(x, Status)
				)
				.SwitchIf(
					_ => string.IsNullOrEmpty(SearchText),
					ifFalse: x => Builder.AddWhereSearch(x, SearchFields, SearchComparison, SearchText)
				)
				.SwitchIf(
					_ => From is not null,
					ifTrue: x => Builder.AddWherePublishedFrom(x, From)
				)
				.SwitchIf(
					_ => To is not null,
					ifTrue: x => Builder.AddWherePublishedTo(x, To)
				)
				.SwitchIf(
					_ => ParentId is not null,
					ifTrue: x => Builder.AddWhereParentId(x, ParentId)
				)
				.SwitchIf(
					_ => Taxonomies.Count > 0,
					ifTrue: x => Builder.AddWhereTaxonomies(x, Taxonomies)
				)
				.SwitchIf(
					_ => CustomFields.Count > 0,
					ifTrue: x => Builder.AddWhereCustomFields(x, CustomFields)
				);
		}
	}
}
