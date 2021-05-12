// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Jeebs.WordPress.Data.Tables;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsOptions"/>
		public sealed record PostsOptions : Options<WpPostId, PostTable>, IQueryPostsOptions
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

			/// <inheritdoc/>
			protected override Expression<Func<PostTable, string>> IdColumn =>
				table => table.PostId;

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal PostsOptions(IWpDb db) : base(db, new PostsPartsBuilder(db.Schema), db.Schema.Post) { }

			/// <summary>
			/// Allow Builder to be injected
			/// </summary>
			/// <param name="db">IWpDb</param>
			/// <param name="builder">IQueryPostsPartsBuilder</param>
			internal PostsOptions(IWpDb db, IQueryPostsPartsBuilder builder) : base(db, builder, db.Schema.Post) { }

			/// <inheritdoc/>
			protected override Option<QueryParts> BuildParts(ITable table, IColumnList cols, IColumn idColumn) =>
				base.BuildParts(
					table, cols, idColumn
				)
				.Bind(
					x => Builder.AddWhereType(x, Type)
				)
				.Bind(
					x => Builder.AddWhereStatus(x, Status)
				)
				.SwitchIf(
					_ => string.IsNullOrEmpty(SearchText),
					ifFalse: x => Builder.AddWhereSearch(x, SearchText, SearchFields, SearchComparison)
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
