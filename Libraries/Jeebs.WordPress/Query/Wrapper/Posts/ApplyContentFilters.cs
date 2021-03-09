// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Reflection;
using Jeebs.Data;
using Jm.WordPress.Query.Wrapper.Posts;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Apply Content Filters to post content
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Post type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="filters">Content Filters</param>
		private static Option<TList> ApplyContentFilters<TList, TModel>(TList posts, ContentFilter[] filters)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there are content filters
			if (filters.Length == 0)
			{
				return posts;
			}

			// Post content field is required as we are expected to apply content filters
			return GetPostContentInfo<TModel>() switch
			{
				Some<Content<TModel>> x when x.Value is var content =>
					Option
						.Wrap(posts)
						.Map(
							x => execute(x, content, filters),
							e => new ApplyContentFiltersExceptionMsg(e)
						),

				_ =>
					Option.None<TList>(new RequiredContentPropertyNotFoundMsg<TModel>())
			};

			//
			// Apply content filters to each post
			//
			static TList execute(TList posts, Content<TModel> content, ContentFilter[] filters)
			{
				foreach (var post in posts)
				{
					// Get post content
					var postContent = content.Get(post);

					// Apply filters
					foreach (var filter in filters)
					{
						postContent = filter.Execute(postContent);
					}

					// Set filtered content
					content.Set(post, postContent);
				}

				return posts;
			}
		}

		private static Option<Content<TModel>> GetPostContentInfo<TModel>() =>
			GetPostContent<TModel>().Map(x => new Content<TModel>(x));

		private class Content<TModel> : PropertyInfo<TModel, string>
		{
			public Content(PropertyInfo info) : base(info) { }
		}
	}
}
