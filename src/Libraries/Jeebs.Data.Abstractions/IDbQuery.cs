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
		/// <inheritdoc cref="IDb.QueryAsync{TModel}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(string query, object? param, CommandType commandType);

		/// <inheritdoc cref="IDb.QueryAsync{TModel}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<TModel>> QuerySingleAsync<TModel>(string query, object? param, CommandType commandType);

		/// <inheritdoc cref="IDb.QuerySingleAsync{TModel}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(string query, object? param);

		/// <inheritdoc cref="IDb.QuerySingleAsync{TModel}(string, object?, CommandType, IDbTransaction?)"/>
		Task<Option<TModel>> QuerySingleAsync<TModel>(string query, object? param);
	}
}
