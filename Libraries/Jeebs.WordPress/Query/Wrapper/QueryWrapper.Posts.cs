// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Querying;
using Jm.WordPress.Query.Wrapper.Posts;

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
		public async Task<Option<List<TModel>>> QueryPostsAsync<TModel>(Action<QueryPosts.Options>? modify = null, params ContentFilter[] filters)
			where TModel : IEntity
		{
			return await Option
				.Wrap(modify)
				.Map(
					GetPostsQuery<TModel>
				)
				.BindAsync(
					x => x.ExecuteQueryAsync()
				)
				.BindAsync(
					async x => x.Count switch
					{
						> 0 =>
							await Process<List<TModel>, TModel>(x, filters),

						_ =>
							x
					}
				);
		}

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
		public async Task<Option<PagedList<TModel>>> QueryPostsAsync<TModel>(long page, Action<QueryPosts.Options>? modify = null, params ContentFilter[] filters)
			where TModel : IEntity
		{
			return await Option
				.Wrap(modify)
				.Map(
					GetPostsQuery<TModel>
				)
				.BindAsync(
					x => x.ExecuteQueryAsync(page)
				)
				.BindAsync(
					async x => x switch
					{
						PagedList<TModel> list when list.Count > 0 =>
							await Process<PagedList<TModel>, TModel>(list, filters),

						PagedList<TModel> list =>
							list,

						_ =>
							Option.None<PagedList<TModel>>(new UnrecognisedPagedListTypeMsg())
					}
				);
		}

		/// <summary>
		/// Get query object
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		private IQuery<TModel> GetPostsQuery<TModel>(Action<QueryPosts.Options>? modify = null) =>
			StartNewQuery()
				.WithModel<TModel>()
				.WithOptions(modify)
				.WithParts(new QueryPosts.Builder<TModel>(db))
				.GetQuery();

		/// <summary>
		/// Process a list of posts
		/// </summary>
		/// <typeparam name="TList">List type</typeparam>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="posts">Posts</param>
		/// <param name="filters">Content filters</param>
		private async Task<Option<TList>> Process<TList, TModel>(TList posts, ContentFilter[] filters)
			where TList : List<TModel>
			where TModel : IEntity =>
			await Option
				.Wrap(posts)
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
			GetMetaDictionary<TModel>().Map(x => new Meta<TModel>(x));

		private class Meta<TModel> : PropertyInfo<TModel, MetaDictionary>
		{
			public Meta(PropertyInfo info) : base(info) { }
		}
	}
}
