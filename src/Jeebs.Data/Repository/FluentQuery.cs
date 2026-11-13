// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Logging;

namespace Jeebs.Data.Repository;

/// <inheritdoc cref="IFluentQuery{TEntity, TId}"/>
public sealed partial record class FluentQuery<TEntity, TId> : IFluentQuery<TEntity, TId>
	where TEntity : IWithId
	where TId : class, IMonad, new()
{
	/// <summary>
	/// Database object.
	/// </summary>
	internal IDb Db { get; private init; }

	/// <summary>
	/// List of errors encountered while building.
	/// </summary>
	internal IList<Failure> Errors { get; private init; } = [];

	/// <summary>
	/// Log (should come with the context of the calling class).
	/// </summary>
	internal ILog Log { get; private init; }

	/// <summary>
	/// Query Parts - used to build the query.
	/// </summary>
	internal IQueryParts Parts { get; private init; }

	/// <summary>
	/// Table that TEntity is mapped to.
	/// </summary>
	internal ITable Table { get; private init; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="db">Database instance.</param>
	/// <param name="mapper">IMapper.</param>
	/// <param name="log">ILog (should come with the context of the calling class).</param>
	internal FluentQuery(IDb db, IEntityMapper mapper, ILog log)
	{
		(Db, Log) = (db, log);

		Table = mapper.GetTableMapFor<TEntity>().Match(
			fFail: f => { Errors.Add(f); return new NullTable(); },
			fOk: x => x.Table
		);

		Parts = new QueryParts(Table);
	}

	/// <summary>
	/// Check the query can run: there were no errors while building it, and it has a WHERE clause.
	/// </summary>
	/// <param name="method">Name of the calling method (for the failure context).</param>
	/// <returns>A failure if the query can't run, otherwise null.</returns>
	internal Failure? CheckParts(string method)
	{
		// Errors are usually from converting a column alias to a column object
		if (Errors.Count > 0)
		{
			return R.Fail("Query errors.", Errors).Ctx(nameof(FluentQuery<,>), method);
		}

		if (Parts.Where.Count == 0 && Parts.WhereCustom.Count == 0)
		{
			return R.Fail("No predicates defined for WHERE clause.").Ctx(nameof(FluentQuery<,>), method);
		}

		return null;
	}

	/// <summary>
	/// Return unchanged query.
	/// </summary>
	/// <returns>Original query.</returns>
	internal IFluentQuery<TEntity, TId> Unchanged() =>
		this;

	/// <summary>
	/// Update <see cref="Parts"/> with a new value.
	/// </summary>
	/// <param name="with">Function to perform the update.</param>
	/// <returns>Updated query.</returns>
	internal IFluentQuery<TEntity, TId> Update(Func<QueryParts, QueryParts> with) =>
		this with { Parts = with(new(Parts)) };
}
