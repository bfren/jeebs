// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Query.Functions;

namespace Jeebs.Data.Query;

/// <summary>
/// IDbQuery extensions for using <see cref="IQueryBuilder"/>.
/// </summary>
public static class DbQueryExtensions
{
	/// <inheritdoc cref="QueryAsync{T}(IDbQuery, ulong, Func{IQueryBuilder, IQueryBuilderWithFrom}, IDbTransaction)"/>
	public static async Task<Maybe<IPagedList<T>>> QueryAsync<T>(
		this IDbQuery @this,
		ulong page,
		Func<IQueryBuilder, IQueryBuilderWithFrom> builder
	)
	{
		using var w = await @this.StartWorkAsync();
		return await QueryAsync<T>(@this, page, builder, w.Transaction).ConfigureAwait(false);
	}

	/// <summary>
	/// Use a fluent <see cref="IQueryBuilder"/> to create a query to run against the database.
	/// </summary>
	/// <typeparam name="T">Return model type</typeparam>
	/// <param name="this">IDbQuery.</param>
	/// <param name="page">Page number.</param>
	/// <param name="builder">Query builder.</param>
	/// <param name="transaction">Database transaction.</param>
	public static Task<Maybe<IPagedList<T>>> QueryAsync<T>(
		this IDbQuery @this,
		ulong page,
		Func<IQueryBuilder, IQueryBuilderWithFrom> builder,
		IDbTransaction transaction
	) =>
		QueryBuilderF.Build<T>(
			builder
		)
		.BindAsync(
			x => @this.QueryAsync<T>(page, x, transaction)
		);

	/// <inheritdoc cref="QueryAsync{T}(IDbQuery, ulong, Func{IQueryBuilder, IQueryBuilderWithFrom}, IDbTransaction)"/>
	public static async Task<Maybe<IEnumerable<T>>> QueryAsync<T>(
		this IDbQuery @this,
		Func<IQueryBuilder, IQueryBuilderWithFrom> builder
	)
	{
		using var w = await @this.StartWorkAsync();
		return await QueryAsync<T>(@this, builder, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc cref="QueryAsync{T}(IDbQuery, ulong, Func{IQueryBuilder, IQueryBuilderWithFrom}, IDbTransaction)"/>
	public static Task<Maybe<IEnumerable<T>>> QueryAsync<T>(
		this IDbQuery @this,
		Func<IQueryBuilder, IQueryBuilderWithFrom> builder,
		IDbTransaction transaction
	) =>
		QueryBuilderF.Build<T>(
			builder
		)
		.BindAsync(
			x => @this.QueryAsync<T>(x, transaction)
		);

	/// <inheritdoc cref="QueryAsync{T}(IDbQuery, ulong, Func{IQueryBuilder, IQueryBuilderWithFrom}, IDbTransaction)"/>
	public static async Task<Maybe<T>> QuerySingleAsync<T>(
		this IDbQuery @this,
		Func<IQueryBuilder, IQueryBuilderWithFrom> builder
	)
	{
		using var w = await @this.StartWorkAsync();
		return await QuerySingleAsync<T>(@this, builder, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc cref="QueryAsync{T}(IDbQuery, ulong, Func{IQueryBuilder, IQueryBuilderWithFrom}, IDbTransaction)"/>
	public static Task<Maybe<T>> QuerySingleAsync<T>(
		this IDbQuery @this,
		Func<IQueryBuilder, IQueryBuilderWithFrom> builder,
		IDbTransaction transaction
	) =>
		QueryBuilderF.Build<T>(
			builder
		)
		.BindAsync(
			x => @this.QuerySingleAsync<T>(x, transaction)
		);
}
