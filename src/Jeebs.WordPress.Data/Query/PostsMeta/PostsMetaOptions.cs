// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Tables;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsMetaOptions"/>
		public sealed record PostsMetaOptions : Options<WpPostMetaId, PostMetaTable>, IQueryPostsMetaOptions
		{
			private new IQueryPostsMetaPartsBuilder Builder =>
				(IQueryPostsMetaPartsBuilder)base.Builder;

			/// <inheritdoc/>
			public WpPostId? PostId { get; init; }

			/// <inheritdoc/>
			public IImmutableList<WpPostId> PostIds { get; init; } =
				new ImmutableList<WpPostId>();

			/// <inheritdoc/>
			public string? Key { get; init; }

			/// <inheritdoc/>
			protected override Expression<Func<PostMetaTable, string>> IdColumn =>
				table => table.PostMetaId;

			internal Expression<Func<PostMetaTable, string>> IdColumnTest =>
				IdColumn;

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal PostsMetaOptions(IWpDb db) : base(db, new PostsMetaPartsBuilder(db.Schema), db.Schema.PostMeta) =>
				Maximum = null;

			/// <inheritdoc/>
			protected override Option<QueryParts> BuildParts(ITable table, IColumnList cols, IColumn idColumn) =>
				base.BuildParts(
					table, cols, idColumn
				)
				.SwitchIf(
					_ => PostId?.Value > 0 || PostIds.Count > 0,
					ifTrue: x => Builder.AddWherePostId(x, PostId, PostIds)
				)
				.SwitchIf(
					_ => !string.IsNullOrEmpty(Key),
					ifTrue: x => Builder.AddWhereKey(x, Key)
				);


		}
	}
}
