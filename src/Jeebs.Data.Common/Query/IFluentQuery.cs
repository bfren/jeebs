// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jeebs.Data.Common.Query;

/// <summary>
/// Build a database query using fluent syntax.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TId">StrongId type.</typeparam>
public interface IFluentQuery<TEntity, TId> : Data.FluentQuery.IFluentQuery<TEntity, TId>
	where TEntity : IWithId
	where TId : class, IUnion, new()
{
	/// <summary>
	/// Return the number of rows matching the query.
	/// </summary>
	/// <param name="transaction">Database transaction.</param>
	/// <returns>Number of matching items.</returns>
	Task<Result<long>> CountAsync(IDbTransaction transaction);

	/// <summary>
	/// Execute the query and return multiple items.
	/// </summary>
	/// <typeparam name="TModel">Return model type.</typeparam>
	/// <param name="transaction">Database transaction.</param>
	/// <returns>List of matching items.</returns>
	Task<Result<IEnumerable<TModel>>> QueryAsync<TModel>(IDbTransaction transaction);

	/// <summary>
	/// Perform the query and return a single item.
	/// </summary>
	/// <typeparam name="TModel">Return model type.</typeparam>
	/// <param name="transaction">Database transaction.</param>
	/// <returns>Single item.</returns>
	Task<Result<TModel>> QuerySingleAsync<TModel>(IDbTransaction transaction);

	/// <summary>
	/// Select a single column and return its value as <typeparamref name="TValue"/>.
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="columnAlias">Column to be selected and returned.</param>
	/// <param name="transaction">Database transaction.</param>
	/// <returns>Query return value.</returns>
	Task<Result<TValue>> ExecuteAsync<TValue>(string columnAlias, IDbTransaction transaction);

	/// <summary>
	/// Select a single column and return its value as <typeparamref name="TValue"/>.
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="aliasSelector">Column to be selected and returned.</param>
	/// <param name="transaction"></param>
	/// <returns>Query return value.</returns>
	Task<Result<TValue>> ExecuteAsync<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, IDbTransaction transaction);
}
