using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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
		/// <param name="r">Result</param>
		/// <param name="filters">Content Filters</param>
		private IR<TList> ApplyContentFilters<TList, TModel>(IOkV<TList> r, ContentFilter[] filters)
			where TList : List<TModel>
			where TModel : IEntity
		{
			// Only proceed if there are content filters
			if (filters.Length == 0)
			{
				return r;
			}

			// Post content field is required as we are expected to apply content filters
			return GetPostContentInfo<TModel>() switch
			{
				Some<Content<TModel>> x when x.Value is var content => r
					.Link()
						.Handle().With<ExecuteContentFiltersExceptionMsg>()
						.Map(okV => execute(okV, content, filters)),
				_ => r.Error().AddMsg().OfType<RequiredContentPropertyNotFoundMsg<TModel>>()
			};

			//
			// Apply content filters to each post
			//
			static IR<TList> execute(IOkV<TList> r, Content<TModel> content, ContentFilter[] filters)
			{
				var posts = r.Value;

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

				return r.OkV(posts);
			}
		}

		private Option<Content<TModel>> GetPostContentInfo<TModel>()
			=> GetPostContent<TModel>().Map(x => new Content<TModel>(x));

		private class Content<TModel> : PropertyInfo<TModel, string>
		{
			public Content(PropertyInfo info) : base(info) { }
		}
	}
}
