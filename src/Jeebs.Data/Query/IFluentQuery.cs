// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Enums;
using StrongId;

namespace Jeebs.Data.Query;

/// <summary>
/// Build a database query using fluent syntax.
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TId">StrongId type</typeparam>
public interface IFluentQuery<TEntity, TId>
	where TEntity : IWithId
	where TId : class, IStrongId, new()
{
	#region Where

	/// <summary>
	/// Add a WHERE predicate (multiple predicates will be added using AND).
	/// </summary>
	/// <param name="columnAlias">Column alias.</param>
	/// <param name="compare">Comparison operator.</param>
	/// <param name="value">Column value.</param>
	IFluentQuery<TEntity, TId> Where(string columnAlias, Compare compare, dynamic? value);

	/// <summary>
	/// Add a WHERE predicate (multiple predicates will be added using AND).
	/// </summary>
	/// <typeparam name="TValue">Column value type</typeparam>
	/// <param name="aliasSelector">Column alias selector.</param>
	/// <param name="compare">Comparison operator.</param>
	/// <param name="value">Column value.</param>
	IFluentQuery<TEntity, TId> Where<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, Compare compare, TValue value);

	/// <summary>
	/// Add a WHERE predicate for the ID column (multiple predicates will be added using AND).
	/// </summary>
	/// <param name="ids">ID(s) to be searched for.</param>
	IFluentQuery<TEntity, TId> WhereId(params TId[] ids);

	/// <summary>
	/// Add a WHERE IN predicate (multiple predicates will be added using AND).
	/// </summary>
	/// <typeparam name="TValue">Column value type</typeparam>
	/// <param name="columnAlias">Column alias.</param>
	/// <param name="values">Array of column values.</param>
	IFluentQuery<TEntity, TId> WhereIn<TValue>(string columnAlias, IEnumerable<TValue> values);

	/// <inheritdoc cref="WhereIn{TValue}(string, IEnumerable{TValue})"/>
	IFluentQuery<TEntity, TId> WhereIn<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, IEnumerable<TValue> values);

	/// <summary>
	/// Add a WHERE NOT IN predicate (multiple predicates will be added using AND).
	/// </summary>
	/// <typeparam name="TValue">Column value type</typeparam>
	/// <param name="columnAlias">Column alias.</param>
	/// <param name="values">Array of column values.</param>
	IFluentQuery<TEntity, TId> WhereNotIn<TValue>(string columnAlias, IEnumerable<TValue> values);

	/// <inheritdoc cref="WhereNotIn{TValue}(Expression{Func{TEntity, TValue}}, IEnumerable{TValue})"/>
	IFluentQuery<TEntity, TId> WhereNotIn<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, IEnumerable<TValue> values);

	/// <summary>
	/// Add a custom where clause with parameters.
	/// </summary>
	/// <param name="clause">Custom clause.</param>
	/// <param name="parameters">Parameters for custom clause.</param>
	IFluentQuery<TEntity, TId> Where(string clause, object parameters);

	#endregion Where

	#region Sort

	/// <summary>
	/// Add an ORDER BY command (multiple commands will be added in order).
	/// </summary>
	/// <param name="columnAlias">Column alias.</param>
	/// <param name="order">Sort order.</param>
	IFluentQuery<TEntity, TId> Sort(string columnAlias, SortOrder order);

	/// <inheritdoc cref="Sort(string, SortOrder)"/>
	IFluentQuery<TEntity, TId> Sort<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, SortOrder order);

	#endregion Sort

	#region Limit

	/// <summary>
	/// The maximum number of records to return.
	/// </summary>
	/// <param name="number"></param>
	IFluentQuery<TEntity, TId> Maximum(ulong number);

	/// <summary>
	/// The number of records to skip.
	/// </summary>
	/// <param name="number"></param>
	IFluentQuery<TEntity, TId> Skip(ulong number);

	#endregion Limit

	#region Execute

	/// <inheritdoc cref="ExecuteAsync{TValue}(string, IDbTransaction)"/>
	Task<Maybe<TValue>> ExecuteAsync<TValue>(string columnAlias);

	/// <summary>
	/// Select a single column and return its value as <typeparamref name="TValue"/>.
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="columnAlias">Column to be selected and returned.</param>
	/// <param name="transaction"></param>
	Task<Maybe<TValue>> ExecuteAsync<TValue>(string columnAlias, IDbTransaction transaction);

	/// <inheritdoc cref="ExecuteAsync{TValue}(Expression{Func{TEntity, TValue}}, IDbTransaction)"/>
	Task<Maybe<TValue>> ExecuteAsync<TValue>(Expression<Func<TEntity, TValue>> aliasSelector);

	/// <inheritdoc cref="ExecuteAsync{TValue}(string, IDbTransaction)"/>
	/// <param name="aliasSelector">Column to be selected and returned.</param>
	/// <param name="transaction"></param>
	Task<Maybe<TValue>> ExecuteAsync<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, IDbTransaction transaction);

	#endregion Execute

	#region Count

	/// <inheritdoc cref="CountAsync(IDbTransaction)"/>
	Task<Maybe<long>> CountAsync();

	/// <summary>
	/// Return the number of rows matching the query.
	/// </summary>
	/// <param name="transaction"></param>
	Task<Maybe<long>> CountAsync(IDbTransaction transaction);

	#endregion Count

	#region Query

	/// <inheritdoc cref="QueryAsync{TModel}(IDbTransaction)"/>/>
	Task<Maybe<IEnumerable<TModel>>> QueryAsync<TModel>();

	/// <summary>
	/// Execute the query and return multiple items.
	/// </summary>
	/// <typeparam name="TModel">Return model type</typeparam>
	/// <param name="transaction"></param>
	Task<Maybe<IEnumerable<TModel>>> QueryAsync<TModel>(IDbTransaction transaction);

	/// <inheritdoc cref="QuerySingleAsync{TModel}(IDbTransaction)"/>
	Task<Maybe<TModel>> QuerySingleAsync<TModel>();

	/// <summary>
	/// Perform the query and return a single item.
	/// </summary>
	/// <typeparam name="TModel">Return model type</typeparam>
	/// <param name="transaction"></param>
	Task<Maybe<TModel>> QuerySingleAsync<TModel>(IDbTransaction transaction);

	#endregion Query
}
