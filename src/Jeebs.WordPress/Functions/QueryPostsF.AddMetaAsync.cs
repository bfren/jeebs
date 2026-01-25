// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Add meta dictionary to posts.
	/// </summary>
	/// <typeparam name="TList">List type</typeparam>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <param name="db">IWpDb.</param>
	/// <param name="w">IUnitOfWork.</param>
	/// <param name="posts">Posts.</param>
	internal static Task<Result<TList>> AddMetaAsync<TList, TModel>(IWpDb db, IUnitOfWork w, TList posts)
		where TList : IEnumerable<TModel>
		where TModel : IWithId<WpPostId, ulong>
	{
		// If there are no posts, do nothing
		if (!posts.Any())
		{
			return R.Wrap(posts).AsTask();
		}

		// Get Meta values
		return GetMetaDictionary<TModel>()
			.MatchAsync(
				ok: x =>
				{
					var ids = posts.Select(p => p.Id).ToImmutableList();
					return from postMeta in QueryPostsMetaF.ExecuteAsync<PostMeta>(db, w, opt => opt with { PostIds = ids })
						   from withMeta in SetMeta(posts, [.. postMeta], x)
						   select posts;
				},
				fail: f =>
				{
					db.Log.Failure(f);
					return posts;
				}
			);
	}
}
