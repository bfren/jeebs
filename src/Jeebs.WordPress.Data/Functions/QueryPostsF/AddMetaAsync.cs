// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
		/// <param name="db">IWpDbQueIWpDbry</param>
		/// <param name="posts">Posts</param>
		internal static Task<Option<TList>> AddMetaAsync<TList, TModel>(IWpDb db, TList posts)
			where TList : IEnumerable<TModel>
			where TModel : IWithId<WpPostId>
		{
			// If there are no posts, do nothing
			if (!posts.Any())
			{
				return Return(posts).AsTask;
			}

			// Get Meta values
			return GetMetaDictionary<TModel>()
				.SwitchAsync(
					some: x =>
						from postMeta in QueryPostsMetaF.ExecuteAsync<PostMeta>(db, opt => opt with
						{
							PostIds = posts.Select(p => p.Id).ToImmutableList()
						})
						from withMeta in SetMeta(posts, postMeta.ToList(), x)
						select posts,
					none: Return(posts)
				);
		}

		/// <summary>
		/// Set meta dictionary property for each post
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="postsMeta">Posts Meta</param>
		/// <param name="metaDict">Meta Dictionary property for <typeparamref name="TModel"/></param>
		internal static Option<TList> SetMeta<TList, TModel>(TList posts, List<PostMeta> postsMeta, Meta<TModel> metaDict)
			where TList : IEnumerable<TModel>
			where TModel : IWithId<WpPostId>
		{
			if (posts.Any() && postsMeta.Count > 0)
			{
				// Add meta to each post
				foreach (var post in posts)
				{
					var postMeta = from m in postsMeta
								   let key = m.Key
								   let value = m.Value
								   where m.PostId == post.Id.Value
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

		internal sealed record PostMeta : WpPostMetaEntity;
	}
}
