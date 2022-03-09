// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Messages;
using Jeebs.StrongId;
using Maybe;
using Maybe.Functions;

namespace Jeebs.Data.Query;

/// <inheritdoc cref="IQueryFluent{TEntity, TId}"/>
public abstract record class QueryFluent
{
	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>No predicates were added when query execution was attempted</summary>
		public sealed record class NoPredicatesMsg : Msg;
	}
}

/// <inheritdoc cref="IQueryFluent{TEntity, TId}"/>
public sealed record class QueryFluent<TEntity, TId> : QueryFluent, IQueryFluent<TEntity, TId>
	where TEntity : IWithId
	where TId : IStrongId
{
	/// <summary>
	/// IRepository
	/// </summary>
	private readonly IRepository<TEntity, TId> repo;

	/// <summary>
	/// List of added predicates
	/// </summary>
	internal IImmutableList<(Expression<Func<TEntity, object>> col, Compare cmp, object val)> Predicates { get; init; } =
		new ImmutableList<(Expression<Func<TEntity, object>>, Compare, object)>();

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="repo">IRepository</param>
	internal QueryFluent(IRepository<TEntity, TId> repo) =>
		this.repo = repo;

	private IQueryFluent<TEntity, TId> With<TValue>(Expression<Func<TEntity, TValue>> column, Compare cmp, object? value)
	{
		// Don't add predicates with a null value
		if (value is null)
		{
			return this;
		}

		// Return with additional predicate
		return this with
		{
			Predicates = Predicates.WithItem((x => column.Compile().Invoke(x) ?? new object(), cmp, value))
		};
	}

	/// <inheritdoc/>
	public IQueryFluent<TEntity, TId> Where<TValue>(Expression<Func<TEntity, TValue>> column, Compare cmp, TValue value) =>
		With(column, cmp, value);

	/// <inheritdoc/>
	public IQueryFluent<TEntity, TId> WhereIn<TValue>(Expression<Func<TEntity, TValue>> column, IEnumerable<TValue> values) =>
		values.Any() switch
		{
			true =>
				With(column, Compare.In, values),

			false =>
				this
		};

	/// <inheritdoc/>
	public IQueryFluent<TEntity, TId> WhereNotIn<TValue>(Expression<Func<TEntity, TValue>> column, IEnumerable<TValue> values) =>
		values.Any() switch
		{
			true =>
				With(column, Compare.NotIn, values),

			false =>
				this
		};

	/// <inheritdoc/>
	public Task<Maybe<IEnumerable<TModel>>> QueryAsync<TModel>() =>
		Predicates.Count switch
		{
			> 0 =>
				repo.QueryAsync<TModel>(Predicates.ToArray()),

			_ =>
				MaybeF.None<IEnumerable<TModel>, M.NoPredicatesMsg>().AsTask
		};

	/// <inheritdoc/>
	public Task<Maybe<TModel>> QuerySingleAsync<TModel>() =>
		Predicates.Count switch
		{
			> 0 =>
				repo.QuerySingleAsync<TModel>(Predicates.ToArray()),

			_ =>
				MaybeF.None<TModel, M.NoPredicatesMsg>().AsTask
		};
}
