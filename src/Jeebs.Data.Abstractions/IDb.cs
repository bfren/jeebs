// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Enables agnostic interaction with a database
	/// </summary>
	public interface IDb
	{
		/// <summary>
		/// Database Client
		/// </summary>
		IDbClient Client { get; }

		/// <summary>
		/// Start a new unit of work
		/// </summary>
		IUnitOfWork UnitOfWork { get; }

		/// <summary>
		/// Run a query and return multiple items
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="query">Query text</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		);

		/// <summary>
		/// Run a query and return a single item
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="query">Query text</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<TModel>> QuerySingleAsync<TModel>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		);

		/// <summary>
		/// Execute a query and return a single value
		/// </summary>
		/// <param name="query">Query text</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<bool>> ExecuteAsync(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		);

		/// <summary>
		/// Execute a query and return a single scalar value
		/// </summary>
		/// <typeparam name="TReturn">Return value type</typeparam>
		/// <param name="query">Query text</param>
		/// <param name="param">Query parameters</param>
		/// <param name="type">Command type</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<TReturn>> ExecuteAsync<TReturn>(
			string query,
			object? param,
			CommandType type,
			IDbTransaction? transaction = null
		);
	}
}
