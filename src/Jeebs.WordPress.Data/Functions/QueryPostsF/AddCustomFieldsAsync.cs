// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.MaybeF;

namespace F.WordPressF.DataF;

public static partial class QueryPostsF
{
	/// <summary>
	/// Add custom fields to posts
	/// </summary>
	/// <typeparam name="TList">List type</typeparam>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="posts">Posts</param>
	internal static Task<Maybe<TList>> AddCustomFieldsAsync<TList, TModel>(IWpDb db, IUnitOfWork w, TList posts)
		where TList : IEnumerable<TModel>
		where TModel : IWithId<WpPostId>
	{
		// If there are no posts, do nothing
		if (!posts.Any())
		{
			return Some(posts).AsTask;
		}

		// Only proceed if there are custom fields, and a meta property for this model
		var fields = GetCustomFields<TModel>();
		if (fields.Count == 0)
		{
			return Some(posts).AsTask;
		}

		// Get terms and add them to the posts
		return GetMetaDictionary<TModel>()
			.BindAsync(
				x => HydrateAsync(db, w, posts, x, fields)
			);
	}
}
