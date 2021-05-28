// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Reflection;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Apply the specified content filters to the list of posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Post type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="filters">Content Filters</param>
		internal static Option<TList> ApplyContentFilters<TList, TModel>(TList posts, IContentFilter[] filters)
			where TList : IEnumerable<TModel>
			where TModel : IWithId<WpPostId>
		{
			// If there are no filters, do nothing
			if (filters.Length == 0)
			{
				return posts;
			}

			// Post content field is required as we are expected to apply content filters
			return GetPostContentInfo<TModel>() switch
			{
				Some<Content<TModel>> x =>
					Return(posts)
						.Map(
							y => ExecuteContentFilters(y, x.Value, filters),
							e => new Msg.ApplyContentFiltersExceptionMsg<TModel>(e)
						),

				_ =>
					None<TList, Msg.RequiredContentPropertyNotFoundMsg<TModel>>()
			};
		}

		/// <summary>
		/// Apply content filters to each post
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Post type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="content">Content Property for <typeparamref name="TModel"/></param>
		/// <param name="filters">Content Filters</param>
		internal static TList ExecuteContentFilters<TList, TModel>(TList posts, Content<TModel> content, IContentFilter[] filters)
			where TList : IEnumerable<TModel>
			where TModel : IWithId<WpPostId>
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

		internal static Option<Content<TModel>> GetPostContentInfo<TModel>()
			where TModel : IWithId<WpPostId>
		{
			return GetPostContent<TModel>().Map(x => new Content<TModel>(x), DefaultHandler);
		}

		internal class Content<TModel> : PropertyInfo<TModel, string>
			where TModel : IWithId<WpPostId>
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
