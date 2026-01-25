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
	/// Add Taxonomies to posts.
	/// </summary>
	/// <typeparam name="TList">List type</typeparam>
	/// <typeparam name="TModel">Post type</typeparam>
	/// <param name="db">IWpDb.</param>
	/// <param name="w">IUnitOfWork.</param>
	/// <param name="posts">Posts.</param>
	internal static Task<Result<TList>> AddTaxonomiesAsync<TList, TModel>(IWpDb db, IUnitOfWork w, TList posts)
		where TList : IEnumerable<TModel>
		where TModel : IWithId<WpPostId, ulong>
	{
		// If there are no posts, do nothing
		if (!posts.Any())
		{
			return R.Wrap(posts).AsTask();
		}

		// Only proceed if there is at least one term list in this model
		var termLists = GetTermLists<TModel>();
		if (termLists.Count == 0)
		{
			return R.Wrap(posts).AsTask();
		}

		// Get terms and add them to the posts
		return QueryPostsTaxonomyF
			.ExecuteAsync<Term>(db, w, opt => opt with
			{
				PostIds = posts.Select(p => p.Id).ToImmutableList()
			})
			.BindAsync(
				x => SetTaxonomies<TList, TModel>(posts, x, termLists)
			);
	}
}
