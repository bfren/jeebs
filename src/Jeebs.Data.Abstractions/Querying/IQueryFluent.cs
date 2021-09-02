// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Build a database query using fluent syntax
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">StrongId type</typeparam>
	public interface IQueryFluent<TEntity, TId>
		where TEntity : IWithId
		where TId : IStrongId
	{
		/// <summary>
		/// Add a WHERE predicate (multiple predicates will be added using AND)
		/// </summary>
		/// <typeparam name="TValue">Column value type</typeparam>
		/// <param name="column">Column selector</param>
		/// <param name="cmp">Comparison operator</param>
		/// <param name="value">Column value</param>
		IQueryFluent<TEntity, TId> Where<TValue>(Expression<Func<TEntity, TValue>> column, Compare cmp, TValue value);

		/// <summary>
		/// Add a WHERE IN predicate (multiple predicates will be added using AND)
		/// </summary>
		/// <typeparam name="TValue">Column value type</typeparam>
		/// <param name="column">Column selector</param>
		/// <param name="values">Column value</param>
		IQueryFluent<TEntity, TId> WhereIn<TValue>(Expression<Func<TEntity, TValue>> column, IEnumerable<TValue> values);

		/// <inheritdoc cref="WhereIn{TValue}(Expression{Func{TEntity, TValue}}, IEnumerable{TValue})"/>
		IQueryFluent<TEntity, TId> WhereNotIn<TValue>(Expression<Func<TEntity, TValue>> column, IEnumerable<TValue> values);

		/// <summary>
		/// Execute the query and return multiple items
		/// </summary>
		/// <typeparam name="TModel">Return model type</typeparam>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>();

		/// <summary>
		/// Perform the query and return a single item
		/// </summary>
		/// <typeparam name="TModel">Return model type</typeparam>
		Task<Option<TModel>> QuerySingleAsync<TModel>();
	}
}
