// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Data;
using Jeebs.Linq;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF;

public static partial class QueryPostsF
{
	/// <summary>
	/// Add meta dictionary to posts
	/// </summary>
	/// <typeparam name="TList">List type</typeparam>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="posts">Posts</param>
	internal static Task<Option<TList>> AddMetaAsync<TList, TModel>(IWpDb db, IUnitOfWork w, TList posts)
		where TList : IEnumerable<TModel>
		where TModel : IWithId<WpPostId>
	{
		// If there are no posts, do nothing
		if (!posts.Any())
		{
			return Some(posts).AsTask;
		}

		// Get Meta values
		return GetMetaDictionary<TModel>()
			.SwitchAsync(
				some: x =>
					from postMeta in QueryPostsMetaF.ExecuteAsync<PostMeta>(db, w, opt => opt with
					{
						PostIds = posts.Select(p => p.Id).ToImmutableList()
					})
					from withMeta in SetMeta(posts, postMeta.ToList(), x)
					select posts,
				none: Some(posts)
			);
	}
}
