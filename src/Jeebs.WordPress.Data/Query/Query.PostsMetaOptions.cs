// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsMetaOptions{TEntity}"/>
		public sealed record PostsMetaOptions<TPostMeta> : Options<TPostMeta, WpPostMetaId>, IQueryPostsMetaOptions<TPostMeta>
			where TPostMeta : WpPostMetaEntity
		{
			/// <inheritdoc/>
			public long? PostId { get; init; }

			/// <inheritdoc/>
			public IImmutableList<long>? PostIds { get; init; }

			/// <inheritdoc/>
			public string? Key { get; init; }

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal PostsMetaOptions(IWpDb db) : base(db) { }

			/// <inheritdoc/>
			protected override Option<QueryParts> GetParts(ITableMap map, IColumnList cols) =>
				base.GetParts(
					map, cols
				)
				.SwitchIf(
					_ => PostId is not null and > 0,
					ifTrue: AddWherePostId
				)
				.SwitchIf(
					_ => PostIds?.Count > 0,
					ifTrue: AddWherePostIds
				)
				.SwitchIf(
					_ => Key is not null && !string.IsNullOrEmpty(Key),
					ifTrue: AddWhereKey
				);

			/// <summary>
			/// Add Where Post ID
			/// </summary>
			/// <param name="parts">QueryParts</param>
			internal Option<QueryParts> AddWherePostId(QueryParts parts)
			{
				// Add Post ID
				if (PostId is long postId)
				{
					return AddWhere(parts, T.PostMeta, p => p.PostId, Compare.Equal, postId);
				}

				// Return
				return parts;
			}

			/// <summary>
			/// Add Where Post IDs
			/// </summary>
			/// <param name="parts">QueryParts</param>
			internal Option<QueryParts> AddWherePostIds(QueryParts parts)
			{
				// Add Post IDs
				if (PostIds is ImmutableList<long> postIds && postIds.Count > 0)
				{
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
