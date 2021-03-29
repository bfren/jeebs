// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Linq;
using static F.OptionF;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// IDbQuery extensions for using <see cref="IQueryBuilder"/>
	/// </summary>
	public static class DbQueryExtensions
	{
		/// <summary>
		/// Build a query and return query parts
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="builder">Query builder</param>
		private static Option<IQueryParts> Build<TModel>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
			Return(
				new QueryBuilder()
			)
			.Map(
				x => (QueryBuilderWithFrom)builder(x),
				e => new Msg.QueryBuilderExceptionMsg(e)
			)
			.Bind(
				x => x.Select<TModel>()
			);

		/// <summary>
		/// Use a fluent <see cref="IQueryBuilder"/> to create a query to run against the database
		/// </summary>
		/// <typeparam name="TModel">Return model type</typeparam>
		/// <param name="this">IDbQuery</param>
		/// <param name="page">Page number</param>
		/// <param name="builder">Query builder</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		public static Task<Option<IPagedList<TModel>>> QueryAsync<TModel>(
			this IDbQuery @this,
			long page,
			Func<IQueryBuilder, IQueryBuilderWithFrom> builder,
			IDbTransaction? transaction = null
		) =>
			Build<TModel>(
				builder
			)
			.BindAsync(
				x => @this.QueryAsync<TModel>(page, x, transaction)
			);

		/// <inheritdoc cref="QueryAsync{TModel}(IDbQuery, long, Func{IQueryBuilder, IQueryBuilderWithFrom}, IDbTransaction?)"/>
		public static Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			this IDbQuery @this,
			Func<IQueryBuilder, IQueryBuilderWithFrom> builder,
			IDbTransaction? transaction = null
		) =>
			Build<TModel>(
				builder
			)
			.BindAsync(
				x => @this.QueryAsync<TModel>(x, transaction)
			);

		/// <inheritdoc cref="QueryAsync{TModel}(IDbQuery, long, Func{IQueryBuilder, IQueryBuilderWithFrom}, IDbTransaction?)"/>
		public static Task<Option<TModel>> QuerySingleAsync<TModel>(
			this IDbQuery @this,
			Func<IQueryBuilder, IQueryBuilderWithFrom> builder,
			IDbTransaction? transaction = null
		) =>
			Build<TModel>(
				builder
			)
			.BindAsync(
				x => @this.QuerySingleAsync<TModel>(x, transaction)
			);

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Error while building query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record QueryBuilderExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
