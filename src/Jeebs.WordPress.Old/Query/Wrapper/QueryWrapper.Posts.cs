﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Querying;
using static F.OptionF;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Posts
		/// <para>Returns:</para>
		/// <para>Result.Failure - if there is an error executing the query, or processing the pages</para>
		/// <para>Result.NotFound - if the query executes successfully but no posts are found</para>
		/// <para>Result.Success - if the query and post processing execute successfully</para>
		/// </summary>
		/// <typeparam name="TModel">Entity type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		/// <param name="filters">[Optional] Content filters to apply to matching posts</param>
		public Task<Option<List<TModel>>> QueryPostsAsync<TModel>(Action<QueryPosts.Options>? modify = null, params ContentFilter[] filters)
			where TModel : IEntity =>
			Return(modify)
				.Bind(
					GetPostsQuery<TModel>
				)
				.BindAsync(
					x => x.ExecuteQueryAsync()
				)
				.BindAsync(
					x => x.Count switch
					{
						> 0 =>
							Process<List<TModel>, TModel>(x, filters),

						_ =>
							Return(x).AsTask
					}
				);

		/// <summary>
		/// Query Posts
		/// <para>Returns:</para>
		/// <para>Result.Failure - if there is an error executing the query, or processing the pages</para>
		/// <para>Result.NotFound - if the query executes successfully but no posts are found</para>
		/// <para>Result.Success - if the query and post processing execute successfully</para>
		/// </summary>
		/// <typeparam name="TModel">Entity type</typeparam>
		/// <param name="page">Page number</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		/// <param name="filters">[Optional] Content filters to apply to matching posts</param>
		public Task<Option<PagedList<TModel>>> QueryPostsAsync<TModel>(long page, Action<QueryPosts.Options>? modify = null, params ContentFilter[] filters)
			where TModel : IEntity =>
			Return(modify)
				.Bind(
					GetPostsQuery<TModel>
				)
				.BindAsync(
					x => x.ExecuteQueryAsync(page)
				)
				.BindAsync(
					x => x switch
					{
						PagedList<TModel> list when list.Count > 0 =>
							Process<PagedList<TModel>, TModel>(list, filters),

						PagedList<TModel> list =>
							Return(list).AsTask,

						_ =>
							None<PagedList<TModel>, Msg.UnrecognisedPagedListTypeMsg>().AsTask
					}
				);

		/// <summary>
		/// Get query object
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		private Option<IQuery<TModel>> GetPostsQuery<TModel>(Action<QueryPosts.Options>? modify = null) =>
			Return(
				() => StartNewQuery()
						.WithModel<TModel>()
						.WithOptions(modify)
						.WithParts(new QueryPosts.Builder<TModel>(db))
						.GetQuery(),
				e => new Msg.GetPostsQueryExceptionMsg(e)
			);

		/// <summary>
		/// Process a list of posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="filters">Content filters</param>
		private Task<Option<TList>> Process<TList, TModel>(TList posts, ContentFilter[] filters)
			where TList : List<TModel>
			where TModel : IEntity =>
			Return(posts)
				.BindAsync(
					AddMetaAsync<TList, TModel>
				)
				.BindAsync(
					AddCustomFieldsAsync<TList, TModel>
				)
				.BindAsync(
					AddTaxonomiesAsync<TList, TModel>
				)
				.BindAsync(
					x => ApplyContentFilters<TList, TModel>(x, filters)
				);

		private static Option<Meta<TModel>> GetMetaDictionaryInfo<TModel>() =>
			GetMetaDictionary<TModel>().Map(x => new Meta<TModel>(x), DefaultHandler);

		private class Meta<TModel> : PropertyInfo<TModel, MetaDictionary>
		{
			public Meta(PropertyInfo info) : base(info) { }
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get posts meta query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetPostsQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Unrecognised <see cref="IPagedList{T}"/> implementation</summary>
			public sealed record UnrecognisedPagedListTypeMsg : IMsg { }
		}

		#region Old PagedList for compatibility

#pragma warning disable RCS1079 // Throwing of new NotImplementedException.

		/// <summary>
		/// Old PagedList for compatibility
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public sealed class PagedList<T> : List<T>, IPagedList<T>
		{
			/// <inheritdoc/>
			public IPagingValues Values { get; }

			Option<T> IImmutableList<T>.this[int index] => throw new NotImplementedException();

			/// <summary>
			/// Create an empty PagedList
			/// </summary>
			public PagedList() =>
				Values = new PagingValues();

			/// <summary>
			/// Create PagedList from a collection of items
			/// </summary>
			/// <param name="values">PagingValues</param>
			/// <param name="collection">Collection</param>
			public PagedList(IPagingValues values, IEnumerable<T> collection) : base(collection) =>
				Values = values;

			/// <summary>
			/// Old PagedList for compatibility
			/// </summary>
			/// <returns></returns>
			public IEnumerable<T> AsEnumerable()
			{
				throw new NotImplementedException();
			}

			/// <summary>
			/// Old PagedList for compatibility
			/// </summary>
			/// <returns></returns>
			public IImmutableList<T> Clone()
			{
				throw new NotImplementedException();
			}

			/// <summary>
			/// Old PagedList for compatibility
			/// </summary>
			/// <returns></returns>
			public List<T> ToList()
			{
				throw new NotImplementedException();
			}

			/// <summary>
			/// Old PagedList for compatibility
			/// </summary>
			/// <param name="add"></param>
			/// <returns></returns>
			public IImmutableList<T> With(T add)
			{
				throw new NotImplementedException();
			}

			/// <summary>
			/// Old PagedList for compatibility
			/// </summary>
			/// <param name="collection"></param>
			/// <returns></returns>
			public IImmutableList<T> WithRange(IEnumerable<T> collection)
			{
				throw new NotImplementedException();
			}

#pragma warning restore RCS1079 // Throwing of new NotImplementedException.

			#endregion
		}
	}
}
