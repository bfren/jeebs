// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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

		/// <inheritdoc cref="IDb.QueryAsync{TModel}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		);

		/// <inheritdoc cref="IDb.QueryAsync{TModel}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			string query,
			object? param,
			IDbTransaction? transaction = null
		);

		/// <summary>
		/// Build a query from <see cref="IQueryParts"/> and return multiple items
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="page">Page number</param>
		/// <param name="parts">Query parts</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<IPagedList<TModel>>> QueryAsync<TModel>(
			long page,
			IQueryParts parts,
			IDbTransaction? transaction = null
		);

		/// <inheritdoc cref="QueryAsync{TModel}(long, IQueryParts, IDbTransaction?)"/>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			IQueryParts parts,
			IDbTransaction? transaction = null
		);

		#endregion

		#region QuerySingleAsync

		/// <inheritdoc cref="IDb.QuerySingleAsync{TModel}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<TModel>> QuerySingleAsync<TModel>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		);

		/// <inheritdoc cref="IDb.QuerySingleAsync{TModel}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<TModel>> QuerySingleAsync<TModel>(
			string query,
			object? param,
			IDbTransaction? transaction = null
		);

		/// <inheritdoc cref="QueryAsync{TModel}(IQueryParts, IDbTransaction?)"/>
		Task<Option<TModel>> QuerySingleAsync<TModel>(
			IQueryParts parts,
			IDbTransaction? transaction = null
		);

		#endregion

		#region ExecuteAsync

		/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<bool>> ExecuteAsync(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		);

		/// <inheritdoc cref="IDb.ExecuteAsync(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<bool>> ExecuteAsync(
			string query,
			object? param,
			IDbTransaction? transaction = null
		);

		/// <inheritdoc cref="IDb.ExecuteAsync{TReturn}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<TReturn>> ExecuteAsync<TReturn>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		);

		/// <inheritdoc cref="IDb.ExecuteAsync{TReturn}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<TReturn>> ExecuteAsync<TReturn>(
			string query,
			object? param,
			IDbTransaction? transaction = null
		);

		#endregion
	}
}
