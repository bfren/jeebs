// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
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
			internal PostsMetaOptions(IWpDb db) : base(db, db.Schema.PostMeta) =>
				Maximum = null;

			/// <inheritdoc/>
			protected override Option<QueryParts> BuildParts(ITable table, IColumnList cols, IColumn idColumn) =>
				base.BuildParts(
					table, cols, idColumn
				)
				.SwitchIf(
					_ => PostId?.Value > 0 || PostIds.Count > 0,
					ifTrue: AddWherePostId
				)
				.SwitchIf(
					_ => !string.IsNullOrEmpty(Key),
					ifTrue: AddWhereKey
				);

			/// <summary>
			/// Add Where Post ID
			/// </summary>
			/// <param name="parts">QueryParts</param>
			internal Option<QueryParts> AddWherePostId(QueryParts parts)
			{
				// Add Post ID EQUAL
				if (PostId?.Value > 0)
				{
					return AddWhere(parts, T.PostMeta, p => p.PostId, Compare.Equal, PostId.Value);
				}

				// Add Post ID IN
				else if (PostIds.Count > 0)
				{
					var postIds = PostIds.Select(p => p.Value);
					return AddWhere(parts, T.PostMeta, p => p.PostId, Compare.In, postIds);
				}

				// Return
				return parts;
			}

			/// <summary>
			/// Add Where Post Status
			/// </summary>
			/// <param name="parts">QueryParts</param>
			internal Option<QueryParts> AddWhereKey(QueryParts parts)
			{
				// Add Key
				if (Key is string key && !string.IsNullOrEmpty(key))
				{
					return AddWhere(parts, T.PostMeta, p => p.Key, Compare.Equal, key);
				}

				// Return
				return parts;
			}
		}
	}
}
