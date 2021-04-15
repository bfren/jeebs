// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;
using static F.WordPressF.DataF.QueryPostsF;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPosts{TEntity}"/>
		public abstract record Posts<TPost, TPostMeta> : IQueryPosts<TPost>
			where TPost : WpPostEntity
			where TPostMeta : WpPostMetaEntity
		{
			private readonly IWpDbQuery query;

			private readonly IQueryPostsMeta<TPostMeta> postsMeta;

			/// <summary>
			/// Inject dependencies
			/// </summary>
			/// <param name="query">IWpDbQuery</param>
			/// <param name="postsMeta">IQueryPostsMeta</param>
			internal Posts(IWpDbQuery query, IQueryPostsMeta<TPostMeta> postsMeta) =>
				(this.query, this.postsMeta) = (query, postsMeta);

			/// <inheritdoc/>
			public Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(IQueryPosts<TPost>.GetOptions opt)
				where TModel : IWithId =>
				Return(
					() => opt(new(query.Db)),
					e => new Msg.ErrorGettingQueryPostsOptionsMsg(e)
				)
				.Bind(
					x => x.GetParts<TModel>()
				)
				.BindAsync(
					x => query.QueryAsync<TModel>(x)
				)
				.BindAsync(
					x => x.Count() switch
					{
						> 0 =>
							Process<IEnumerable<TModel>, TModel>(x),

						_ =>
							Return(x).AsTask
					}
				);

			/// <inheritdoc/>
			public Task<Option<IPagedList<TModel>>> ExecuteAsync<TModel>(long page, IQueryPosts<TPost>.GetOptions opt)
				where TModel : IWithId =>
				Return(
					() => opt(new(query.Db)),
					e => new Msg.ErrorGettingQueryPostsOptionsMsg(e)
				)
				.Bind(
					x => x.GetParts<TModel>()
				)
				.BindAsync(
					x => query.QueryAsync<TModel>(page, x)
				)
				.BindAsync(
					x => x switch
					{
						PagedList<TModel> when x.Count > 0 =>
							Process<IPagedList<TModel>, TModel>(x),

						PagedList<TModel> =>
							Return(x).AsTask,

						_ =>
							None<IPagedList<TModel>, Msg.UnrecognisedPagedListTypeMsg>().AsTask
					}
				);

			/// <summary>
			/// Process a list of posts, adding meta / taxonomies / custom fields as required
			/// </summary>
			/// <typeparam name="TList">List type</typeparam>
			/// <typeparam name="TModel">Model type</typeparam>
			/// <param name="posts">Posts</param>
			internal Task<Option<TList>> Process<TList, TModel>(TList posts)
				where TList : IEnumerable<TModel>
				where TModel : IWithId =>
				Return(
					posts
				)
				.BindAsync(
					x => AddMetaAsync<TList, TModel>(query, x)
				)
				.BindAsync(
					x => AddCustomFieldsAsync<TList, TModel>(query, x)
				)
				.BindAsync(
					x => AddTaxonomiesAsync<TList, TModel>(query, x)
				);
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get posts query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryPostsOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Unrecognised <see cref="IPagedList{T}"/> implementation</summary>
			public sealed record UnrecognisedPagedListTypeMsg : IMsg { }
		}
	}
}
