﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Linq;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Add meta dictionary to posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <typeparam name="TPostMeta">Post Meta Entity type</typeparam>
		/// <param name="query">IWpDbQuery</param>
		/// <param name="posts">Posts</param>
		internal static Task<Option<TList>> AddMetaAsync<TList, TModel, TPostMeta>(IWpDbQuery query, TList posts)
			where TList : IEnumerable<TModel>
			where TModel : IWithId
			where TPostMeta : WpPostMetaEntity =>
			GetMetaDictionary<TModel>()
			.SwitchAsync(
				some: x =>
					from postMeta in QueryPostsMetaF.ExecuteAsync<TPostMeta, TPostMeta>(query, opt => opt with
					{
						PostIds = posts.Select(p => p.Id.Value).ToImmutableList()
					})
					from withMeta in SetMeta<TList, TModel, TPostMeta>(posts, postMeta.ToList(), x)
					select posts,
				none: Return(posts)
			);

		/// <summary>
		/// Set meta dictionary property for each post
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <typeparam name="TPostMeta">Post Meta Entity type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="postsMeta">Posts Meta</param>
		/// <param name="metaDict">Meta Dictionary property for <typeparamref name="TModel"/></param>
		internal static Option<TList> SetMeta<TList, TModel, TPostMeta>(TList posts, List<TPostMeta> postsMeta, Meta<TModel> metaDict)
			where TList : IEnumerable<TModel>
			where TModel : IWithId
			where TPostMeta : WpPostMetaEntity
		{
			if (postsMeta.Count > 0)
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
}
