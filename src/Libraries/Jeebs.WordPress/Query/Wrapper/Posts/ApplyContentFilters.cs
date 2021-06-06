// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Reflection;
using Jeebs.WordPress.Data;
using static F.OptionF;

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
					Return(posts)
						.Map(
							x => execute(x, content, filters),
							e => new Msg.ApplyContentFiltersExceptionMsg<TModel>(e)
						),

				_ =>
					None<TList, Msg.RequiredContentPropertyNotFoundMsg<TModel>>()
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
			GetPostContent<TModel>().Map(x => new Content<TModel>(x), DefaultHandler);

		private class Content<TModel> : PropertyInfo<TModel, string>
		{
			public Content(PropertyInfo info) : base(info) { }
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>An exception occured while applying content filters to posts</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record ApplyContentFiltersExceptionMsg<T>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Required Content property not found</summary>
			/// <typeparam name="T">Post Model type</typeparam>
			public sealed record RequiredContentPropertyNotFoundMsg<T> : IMsg { }
		}
	}
}
