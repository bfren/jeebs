// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Query;

namespace Jeebs.Data;

/// <summary>
/// General database query functionality.
/// </summary>
public interface IDbQuery
{
	/// <summary>
	/// Begin a new Unit of Work.
	/// </summary>
	Task<IUnitOfWork> StartWorkAsync();

	#region QueryAsync

	/// <inheritdoc cref="IDb.QueryAsync{T}(string, object?, CommandType)"/>
	Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type);

	/// <inheritdoc cref="IDb.QueryAsync{T}(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <inheritdoc cref="IDb.QueryAsync{T}(string, object?, CommandType)"/>
	Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param);

	/// <inheritdoc cref="IDb.QueryAsync{T}(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<IEnumerable<T>>> QueryAsync<T>(string query, object? param, IDbTransaction transaction);

	/// <inheritdoc cref="QueryAsync{T}(ulong, IQueryParts, IDbTransaction)"/>
	Task<Maybe<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts);

	/// <summary>
	/// Build a query from <see cref="IQueryParts"/> and return multiple items.
	/// </summary>
	/// <typeparam name="T">Return value type</typeparam>
	/// <param name="page">Page number.</param>
	/// <param name="parts">Query parts.</param>
	/// <param name="transaction">[Optional] Database transaction.</param>
	Task<Maybe<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts, IDbTransaction transaction);

	/// <inheritdoc cref="QueryAsync{T}(ulong, IQueryParts)"/>
	Task<Maybe<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts);

	/// <inheritdoc cref="QueryAsync{T}(ulong, IQueryParts, IDbTransaction)"/>
	Task<Maybe<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts, IDbTransaction transaction);

	#endregion QueryAsync

	#region QuerySingleAsync

	/// <inheritdoc cref="IDb.QuerySingleAsync{T}(string, object?, CommandType)"/>
	Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, CommandType type);

	/// <inheritdoc cref="IDb.QuerySingleAsync{T}(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <inheritdoc cref="IDb.QuerySingleAsync{T}(string, object?, CommandType)"/>
	Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param);

	/// <inheritdoc cref="IDb.QuerySingleAsync{T}(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<T>> QuerySingleAsync<T>(string query, object? param, IDbTransaction transaction);

	/// <inheritdoc cref="QueryAsync{T}(IQueryParts)"/>
	Task<Maybe<T>> QuerySingleAsync<T>(IQueryParts parts);

	/// <inheritdoc cref="QueryAsync{T}(IQueryParts, IDbTransaction)"/>
	Task<Maybe<T>> QuerySingleAsync<T>(IQueryParts parts, IDbTransaction transaction);

	#endregion QuerySingleAsync

	#region ExecuteAsync

	/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType)"/>
	Task<Maybe<bool>> ExecuteAsync(string query, object? param, CommandType type);

	/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType)"/>
	Task<Maybe<bool>> ExecuteAsync(string query, object? param);

	/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<bool>> ExecuteAsync(string query, object? param, IDbTransaction transaction);

	/// <inheritdoc cref="IDb.ExecuteAsync{T}(string, object?, CommandType)"/>
	Task<Maybe<T>> ExecuteAsync<T>(string query, object? param, CommandType type);

	/// <inheritdoc cref="IDb.ExecuteAsync{T}(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<T>> ExecuteAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

	/// <inheritdoc cref="IDb.ExecuteAsync{TReturn}(string, object?, CommandType)"/>
	Task<Maybe<T>> ExecuteAsync<T>(string query, object? param);

	/// <inheritdoc cref="IDb.ExecuteAsync{TReturn}(string, object?, CommandType, IDbTransaction)"/>
	Task<Maybe<T>> ExecuteAsync<T>(string query, object? param, IDbTransaction transaction);

	#endregion ExecuteAsync
}
