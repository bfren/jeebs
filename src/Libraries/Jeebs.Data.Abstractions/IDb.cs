﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
		/// Run a query and return multiple models
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="query">Query text</param>
		/// <param name="parameters">Query parameters</param>
		/// <param name="type">Command type</param>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(string query, object? parameters, CommandType type);

		/// <summary>
		/// Run a query and return a single model
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="query">Query text</param>
		/// <param name="parameters">Query parameters</param>
		/// <param name="type">Command type</param>
		Task<Option<TModel>> QuerySingleAsync<TModel>(string query, object? parameters, CommandType type);

		/// <summary>
		/// Execute a query and return a single value
		/// </summary>
		/// <param name="query">Query text</param>
		/// <param name="parameters">Query parameters</param>
		/// <param name="type">Command type</param>
		Task<Option<bool>> ExecuteAsync(string query, object? parameters, CommandType type);

		/// <summary>
		/// Execute a query and return a single scalar value
		/// </summary>
		/// <typeparam name="TReturn">Return value type</typeparam>
		/// <param name="query">Query text</param>
		/// <param name="parameters">Query parameters</param>
		/// <param name="type">Command type</param>
		Task<Option<TReturn>> ExecuteAsync<TReturn>(string query, object? parameters, CommandType type);
	}
}