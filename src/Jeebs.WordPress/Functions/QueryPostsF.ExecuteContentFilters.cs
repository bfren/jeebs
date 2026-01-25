// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using Jeebs.WordPress.ContentFilters;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Apply content filters to each post.
	/// </summary>
	/// <typeparam name="TList">List type.</typeparam>
	/// <typeparam name="TModel">Post Model type.</typeparam>
	/// <param name="posts">Posts.</param>
	/// <param name="content">Content Property for <typeparamref name="TModel"/>.</param>
	/// <param name="filters">Content Filters.</param>
	internal static TList ExecuteContentFilters<TList, TModel>(TList posts, Content<TModel> content, IContentFilter[] filters)
		where TList : IEnumerable<TModel>
	{
		// Don't do anything if there are no posts or no filters
		if (!posts.Any() || filters.Length == 0)
		{
			return posts;
		}

		// Loop through each post and apply filters
		foreach (var post in posts)
		{
			// Get post content
			_ = content.Get(post).IfSome(postContent =>
			{
				// Apply filters
				foreach (var filter in filters)
				{
					postContent = filter.Execute(postContent);
				}

				// Set filtered content
				content.Set(post, postContent);
			});
		}

		return posts;
	}
}
