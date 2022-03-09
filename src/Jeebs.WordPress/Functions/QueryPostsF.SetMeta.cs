// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using Jeebs.WordPress.Entities;
using Maybe;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Set meta dictionary property for each post
	/// </summary>
	/// <typeparam name="TList">List type</typeparam>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <param name="posts">Posts</param>
	/// <param name="postsMeta">Posts Meta</param>
	/// <param name="metaDict">Meta Dictionary property for <typeparamref name="TModel"/></param>
	internal static Maybe<TList> SetMeta<TList, TModel>(TList posts, List<PostMeta> postsMeta, Meta<TModel> metaDict)
		where TList : IEnumerable<TModel>
		where TModel : Id.IWithId<WpPostId>
	{
		if (posts.Any() && postsMeta.Count > 0)
		{
			// Add meta to each post
			foreach (var post in posts)
			{
				var postMeta = from m in postsMeta
							   let key = m.Key
							   let value = m.Value
							   where m.PostId == post.Id
							   && !string.IsNullOrEmpty(key)
							   && !string.IsNullOrEmpty(value)
							   select new KeyValuePair<string, string>(key, value);

				if (!postMeta.Any())
				{
					continue;
				}

				// Set the value of the meta property
				metaDict.Set(post, new MetaDictionary(postMeta));
			}
		}

		return posts;
	}
}
