// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Linq.Expressions;
using Jeebs.Data.Enums;
using static F.OptionF;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryFluent{TEntity, TId}"/>
	public abstract record class QueryFluent
	{
		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>No predicates were added when query execution was attempted</summary>
			public sealed record class NoPredicatesMsg : IMsg { }
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
				Predicates = Predicates.With((x => column.Compile().Invoke(x) ?? new object(), cmp, value))
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
		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>() =>
			Predicates.Count switch
			{
				> 0 =>
					repo.QueryAsync<TModel>(Predicates.ToArray()),

				_ =>
					None<IEnumerable<TModel>, Msg.NoPredicatesMsg>().AsTask
			};

		/// <inheritdoc/>
		public Task<Option<TModel>> QuerySingleAsync<TModel>() =>
			Predicates.Count switch
			{
				> 0 =>
					repo.QuerySingleAsync<TModel>(Predicates.ToArray()),

				_ =>
					None<TModel, Msg.NoPredicatesMsg>().AsTask
			};
	}
}
