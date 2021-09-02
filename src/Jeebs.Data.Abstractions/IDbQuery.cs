// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Querying;

namespace Jeebs.Data
{
	/// <summary>
	/// General database query functionality
	/// </summary>
	public interface IDbQuery
	{
		/// <inheritdoc cref="IDb.UnitOfWork"/>
		IUnitOfWork UnitOfWork { get; }

		#region QueryAsync

		/// <inheritdoc cref="IDb.QueryAsync{T}(string, object?, CommandType)"/>
		Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type);

		/// <inheritdoc cref="IDb.QueryAsync{T}(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

		/// <inheritdoc cref="IDb.QueryAsync{T}(string, object?, CommandType)"/>
		Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param);

		/// <inheritdoc cref="IDb.QueryAsync{T}(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<IEnumerable<T>>> QueryAsync<T>(string query, object? param, IDbTransaction transaction);

		/// <inheritdoc cref="QueryAsync{T}(ulong, IQueryParts, IDbTransaction)"/>
		Task<Option<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts);

		/// <summary>
		/// Build a query from <see cref="IQueryParts"/> and return multiple items
		/// </summary>
		/// <typeparam name="T">Return value type</typeparam>
		/// <param name="page">Page number</param>
		/// <param name="parts">Query parts</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts, IDbTransaction transaction);

		/// <inheritdoc cref="QueryAsync{T}(ulong, IQueryParts)"/>
		Task<Option<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts);

		/// <inheritdoc cref="QueryAsync{T}(ulong, IQueryParts, IDbTransaction)"/>
		Task<Option<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts, IDbTransaction transaction);

		#endregion

		#region QuerySingleAsync

		/// <inheritdoc cref="IDb.QuerySingleAsync{T}(string, object?, CommandType)"/>
		Task<Option<T>> QuerySingleAsync<T>(string query, object? param, CommandType type);

		/// <inheritdoc cref="IDb.QuerySingleAsync{T}(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

		/// <inheritdoc cref="IDb.QuerySingleAsync{T}(string, object?, CommandType)"/>
		Task<Option<T>> QuerySingleAsync<T>(string query, object? param);

		/// <inheritdoc cref="IDb.QuerySingleAsync{T}(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<T>> QuerySingleAsync<T>(string query, object? param, IDbTransaction transaction);

		/// <inheritdoc cref="QueryAsync{T}(IQueryParts)"/>
		Task<Option<T>> QuerySingleAsync<T>(IQueryParts parts);

		/// <inheritdoc cref="QueryAsync{T}(IQueryParts, IDbTransaction)"/>
		Task<Option<T>> QuerySingleAsync<T>(IQueryParts parts, IDbTransaction transaction);

		#endregion

		#region ExecuteAsync

		/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType)"/>
		Task<Option<bool>> ExecuteAsync(string query, object? param, CommandType type);

		/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction);

		/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType)"/>
		Task<Option<bool>> ExecuteAsync(string query, object? param);

		/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<bool>> ExecuteAsync(string query, object? param, IDbTransaction transaction);

		/// <inheritdoc cref="IDb.ExecuteAsync{T}(string, object?, CommandType)"/>
		Task<Option<T>> ExecuteAsync<T>(string query, object? param, CommandType type);

		/// <inheritdoc cref="IDb.ExecuteAsync{T}(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<T>> ExecuteAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction);

		/// <inheritdoc cref="IDb.ExecuteAsync{TReturn}(string, object?, CommandType)"/>
		Task<Option<T>> ExecuteAsync<T>(string query, object? param);

		/// <inheritdoc cref="IDb.ExecuteAsync{TReturn}(string, object?, CommandType, IDbTransaction)"/>
		Task<Option<T>> ExecuteAsync<T>(string query, object? param, IDbTransaction transaction);

		#endregion
	}
}
