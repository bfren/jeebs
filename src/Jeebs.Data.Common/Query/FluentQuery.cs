// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Data.Map;
using Jeebs.Data.QueryBuilder;
using Jeebs.Logging;

namespace Jeebs.Data.Common.Query;

/// <inheritdoc cref="IFluentQuery{TEntity, TId}"/>
public abstract record class FluentQuery { }

/// <inheritdoc cref="IFluentQuery{TEntity, TId}"/>
public sealed partial record class FluentQuery<TEntity, TId> : FluentQuery, IFluentQuery<TEntity, TId>
	where TEntity : IWithId
	where TId : class, IUnion, new()
{
	/// <summary>
	/// Database object.
	/// </summary>
	internal IDb Db { get; private init; }

	/// <summary>
	/// List of errors encountered while building.
	/// </summary>
	internal IList<FailureValue> Errors { get; private init; } = [];

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
			fail: f => { Errors.Add(f); return new NullTable(); },
			ok: x => x.Table
		);

		Parts = new QueryParts(Table);
	}

	/// <summary>
	/// Update <see cref="Parts"/> with a new value.
	/// </summary>
	/// <param name="with">Function to perform the update.</param>
	internal IFluentQuery<TEntity, TId> Update(Func<QueryParts, QueryParts> with) =>
		this with { Parts = with(new(Parts)) };
}
