// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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
		/// <param name="r">Result</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		/// <param name="filters">[Optional] Content filters to apply to matching posts</param>
		public async Task<IR<List<TModel>>> QueryPostsAsync<TModel>(IOk r, Action<QueryPosts.Options>? modify = null, params ContentFilter[] filters)
			where TModel : IEntity
		{
			// Get query
			var query = GetPostsQuery<TModel>(modify);

			// Execute query
			return await query.ExecuteQueryAsync(r).ConfigureAwait(false) switch
			{
				IOkV<List<TModel>> x when x.Value.Count == 0 =>
					x,

				IOkV<List<TModel>> x =>
					Process<List<TModel>, TModel>(x, filters).Await(),

				{ } x =>
					x.Error(),
			};
		}

		/// <summary>
		/// Query Posts
		/// <para>Returns:</para>
		/// <para>Result.Failure - if there is an error executing the query, or processing the pages</para>
		/// <para>Result.NotFound - if the query executes successfully but no posts are found</para>
		/// <para>Result.Success - if the query and post processing execute successfully</para>
		/// </summary>
		/// <typeparam name="TModel">Entity type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="page">Page number</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		/// <param name="filters">[Optional] Content filters to apply to matching posts</param>
		public async Task<IR<PagedList<TModel>>> QueryPostsAsync<TModel>(IOk r, long page, Action<QueryPosts.Options>? modify = null, params ContentFilter[] filters)
			where TModel : IEntity
		{
			// Get query
			var query = GetPostsQuery<TModel>(modify);

			// Execute query
			return await query.ExecuteQueryAsync(r, page).ConfigureAwait(false) switch
			{
				IOkV<PagedList<TModel>> x when x.Value.Count == 0 =>
					x,

				IOkV<PagedList<TModel>> x =>
					Process<PagedList<TModel>, TModel>(x, filters).Await(),

				{ } x =>
					x.Error<PagedList<TModel>>(),
			};
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
		/// <param name="r">Result</param>
		/// <param name="filters">Content filters</param>
		private async Task<IR<TList>> Process<TList, TModel>(IOkV<TList> r, ContentFilter[] filters)
			where TList : List<TModel>
			where TModel : IEntity =>
			r
				.Link()
					.Handle().With<AddMetaExceptionMsg>()
					.MapAsync(AddMetaAsync<TList, TModel>).Await()
				.Link()
					.Handle().With<AddCustomFieldsExceptionMsg>()
					.MapAsync(AddCustomFieldsAsync<TList, TModel>).Await()
				.Link()
					.Handle().With<AddTaxonomiesExceptionMsg>()
					.MapAsync(AddTaxonomiesAsync<TList, TModel>).Await()
				.Link()
					.Handle().With<ApplyContentFiltersExceptionMsg>()
					.Map(okV => ApplyContentFilters<TList, TModel>(okV, filters));

		private static Option<Meta<TModel>> GetMetaDictionaryInfo<TModel>() =>
			GetMetaDictionary<TModel>().Map(x => new Meta<TModel>(x));

		private class Meta<TModel> : PropertyInfo<TModel, MetaDictionary>
		{
			public Meta(PropertyInfo info) : base(info) { }
		}
	}
}
