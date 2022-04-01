// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Logging;
using Jeebs.Messages;
using Jeebs.Reflection;
using StrongId;

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
	where TEntity : IWithId<TId>
	where TId : class, IStrongId, new()
{
	/// <summary>
	/// IRepository
	/// </summary>
	private IDb Db { get; init; }

	private ILog Log { get; init; }

	/// <summary>
	/// List of added predicates
	/// </summary>
	internal IImmutableList<(string col, Compare cmp, dynamic val)> Predicates { get; init; } =
		new ImmutableList<(string, Compare, dynamic)>();

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="db"></param>
	/// <param name="log"></param>
	internal QueryFluent(IDb db, ILog log) =>
		(Db, Log) = (db, log);

	private IQueryFluent<TEntity, TId> With(string column, Compare cmp, dynamic? value)
	{
		// Don't add predicates with a null value
		if (value is null)
		{
			return this;
		}

		// Add new predicate
		return this with
		{
			Predicates = Predicates.WithItem((column, cmp, value))
		};
	}

	private IQueryFluent<TEntity, TId> With<TValue>(Expression<Func<TEntity, TValue>> column, Compare cmp, dynamic? value) =>
		column.GetPropertyInfo()
			.Switch(
				some: x => With(x.Name, cmp, value),
				none: this
			);

	/// <inheritdoc/>
	public IQueryFluent<TEntity, TId> Where(string column, Compare cmp, dynamic? value) =>
		With(column, cmp, value);

	/// <inheritdoc/>
	public IQueryFluent<TEntity, TId> Where<TValue>(Expression<Func<TEntity, TValue>> column, Compare cmp, TValue value) =>
		With(column, cmp, value);

	/// <inheritdoc/>
	public IQueryFluent<TEntity, TId> WhereIn<TValue>(string column, IEnumerable<TValue> values) =>
		values.Any() switch
		{
			true =>
				With(column, Compare.In, values),

			false =>
				this
		};

	/// <inheritdoc/>
	public IQueryFluent<TEntity, TId> WhereIn<TValue>(Expression<Func<TEntity, TValue>> column, IEnumerable<TValue> values) =>
		column.GetPropertyInfo()
			.Switch(
				some: x => WhereIn(x.Name, values),
				none: this
			);

	/// <inheritdoc/>
	public IQueryFluent<TEntity, TId> WhereNotIn<TValue>(string column, IEnumerable<TValue> values) =>
		values.Any() switch
		{
			true =>
				With(column, Compare.NotIn, values),

			false =>
				this
		};

	/// <inheritdoc/>
	public IQueryFluent<TEntity, TId> WhereNotIn<TValue>(Expression<Func<TEntity, TValue>> column, IEnumerable<TValue> values) =>
		column.GetPropertyInfo()
			.Switch(
				some: x => WhereNotIn(x.Name, values),
				none: this
			);

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<TModel>>> QueryAsync<TModel>()
	{
		using var w = Db.UnitOfWork;
		return await QueryAsync<TModel>(w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<IEnumerable<TModel>>> QueryAsync<TModel>(IDbTransaction transaction) =>
		Predicates.Count switch
		{
			> 0 =>
				Db.Client
					.GetQuery<TEntity, TModel>(
						Predicates.ToArray()
					)
					.BindAsync(
						x => Db.QueryAsync<TModel>(x.query, x.param, CommandType.Text, transaction)
					),

			_ =>
				F.None<IEnumerable<TModel>, M.NoPredicatesMsg>().AsTask
		};

	/// <inheritdoc/>
	public async Task<Maybe<TModel>> QuerySingleAsync<TModel>()
	{
		using var w = Db.UnitOfWork;
		return await QuerySingleAsync<TModel>(w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<TModel>> QuerySingleAsync<TModel>(IDbTransaction transaction) =>
		QueryAsync<TModel>(
			transaction
		)
		.UnwrapAsync(
			x => x.SingleValue<TModel>()
		);
}
