// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.WordPress.Entities;
using Maybe;

namespace Jeebs.WordPress;

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
			new Column(T.PostMeta, T.PostMeta.Id, nameof(T.PostMeta.Id));

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
		public Maybe<QueryParts> AddWherePostId(QueryParts parts, WpPostId? postId, IImmutableList<WpPostId> postIds)
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
		public Maybe<QueryParts> AddWhereKey(QueryParts parts, string? key)
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
