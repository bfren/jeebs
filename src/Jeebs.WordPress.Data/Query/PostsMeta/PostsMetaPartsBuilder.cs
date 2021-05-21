// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Linq;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsPartsBuilder"/>
		public sealed class PostsMetaPartsBuilder : PartsBuilder<WpPostMetaId>, IQueryPostsMetaPartsBuilder
		{
			/// <inheritdoc/>
			public override ITable Table =>
				T.PostMeta;

			/// <inheritdoc/>
			public override IColumn IdColumn =>
				new Column(T.PostMeta.GetName(), T.PostMeta.PostMetaId, nameof(T.PostMeta.PostMetaId));

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			internal PostsMetaPartsBuilder(IWpDbSchema schema) : base(schema) { }

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="extract">IExtract</param>
			/// <param name="schema">IWpDbSchema</param>
			internal PostsMetaPartsBuilder(IExtract extract, IWpDbSchema schema) : base(extract, schema) { }

			/// <inheritdoc/>
			public Option<QueryParts> AddWherePostId(QueryParts parts, WpPostId? postId, IImmutableList<WpPostId> postIds)
			{
				// Add Post ID EQUAL
				if (postId?.Value > 0)
				{
					return AddWhere(parts, T.PostMeta, p => p.PostId, Compare.Equal, postId.Value);
				}

				// Add Post ID IN
				else if (postIds.Count > 0)
				{
					var postIdValues = postIds.Select(p => p.Value);
					return AddWhere(parts, T.PostMeta, p => p.PostId, Compare.In, postIdValues);
				}

				// Return
				return parts;
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereKey(QueryParts parts, string? key)
			{
				// Add Key
				if (!string.IsNullOrEmpty(key))
				{
					return AddWhere(parts, T.PostMeta, p => p.Key, Compare.Equal, key);
				}

				// Return
				return parts;
			}
		}
	}
}
