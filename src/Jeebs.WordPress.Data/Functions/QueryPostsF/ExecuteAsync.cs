﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Querying;
using static F.OptionF;

namespace F.WordPressF.DataF;

public static partial class QueryPostsF
{
	/// <inheritdoc cref="ExecuteAsync{TModel}(IWpDb, IUnitOfWork, ulong, GetPostsOptions, IContentFilter[])"/>
	internal static Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(
		IWpDb db,
		IUnitOfWork w,
		GetPostsOptions opt,
		params IContentFilter[] filters
	)
		where TModel : IWithId<WpPostId>
	{
		return
			GetQueryParts<TModel>(
				db, opt
			)
			.BindAsync(
				x => db.Query.QueryAsync<TModel>(x, w.Transaction)
			)
			.BindAsync(
				x => x.Count() switch
				{
					> 0 =>
						Process<IEnumerable<TModel>, TModel>(db, w, x, filters),

					_ =>
						Some(x).AsTask
				}
			);
	}

	/// <summary>
	/// Run a query and return multiple items with paging
	/// </summary>
	/// <typeparam name="TModel">Return value type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="page">Page number</param>
	/// <param name="opt">Function to return query options</param>
	/// <param name="filters">Optional content filters to apply</param>
	internal static Task<Option<IPagedList<TModel>>> ExecuteAsync<TModel>(
		IWpDb db,
		IUnitOfWork w,
		ulong page,
		GetPostsOptions opt,
		params IContentFilter[] filters
	)
		where TModel : IWithId<WpPostId>
	{
		return
			GetQueryParts<TModel>(
				db, opt
			)
			.BindAsync(
				x => db.Query.QueryAsync<TModel>(page, x, w.Transaction)
			)
			.BindAsync(
				x => x switch
				{
					PagedList<TModel> when x.Count > 0 =>
						Process<IPagedList<TModel>, TModel>(db, w, x, filters),

					PagedList<TModel> =>
						Some(x).AsTask,

					_ =>
						None<IPagedList<TModel>, M.UnrecognisedPagedListTypeMsg>().AsTask
				}
			);
	}

	public static partial class M
	{
		/// <summary>Unable to get posts query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingQueryPostsOptionsMsg(Exception Value) : ExceptionMsg;

		/// <summary>Unrecognised <see cref="IPagedList{T}"/> implementation</summary>
		public sealed record class UnrecognisedPagedListTypeMsg : Msg;
	}
}
