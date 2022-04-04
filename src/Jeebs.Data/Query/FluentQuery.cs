// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Data.Map;
using Jeebs.Logging;
using Jeebs.Messages;
using StrongId;

namespace Jeebs.Data.Query;

/// <inheritdoc cref="IFluentQuery{TEntity, TId}"/>
public abstract record class FluentQuery
{
	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>No predicates were added when query execution was attempted</summary>
		public sealed record class NoPredicatesMsg : Msg;

		/// <summary>Trying to add an empty string as a 'where' clause is not permitted</summary>
		public sealed record class TryingToAddEmptyClauseToWhereMsg : Msg;

		/// <summary>Error creating parameter dictionary for a 'where' clause</summary>
		public sealed record class UnableToAddParametersToWhereMsg : Msg;
	}
}

/// <inheritdoc cref="IFluentQuery{TEntity, TId}"/>
public sealed partial record class FluentQuery<TEntity, TId> : FluentQuery, IFluentQuery<TEntity, TId>
	where TEntity : IWithId<TId>
	where TId : class, IStrongId, new()
{
	/// <summary>
	/// Database object
	/// </summary>
	internal IDb Db { get; private init; }

	/// <summary>
	/// List of errors encountered while building
	/// </summary>
	internal IList<IMsg> Errors { get; private init; }

	/// <summary>
	/// Log (should come with the context of the calling class)
	/// </summary>
	internal ILog Log { get; private init; }

	/// <summary>
	/// Query Parts - used to build the query
	/// </summary>
	internal IQueryParts Parts { get; private init; }

	/// <summary>
	/// Table that TEntity is mapped to
	/// </summary>
	internal ITable Table { get; private init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="db">Database instance</param>
	/// <param name="mapper">IMapper</param>
	/// <param name="log">ILog (should come with the context of the calling class)</param>
	internal FluentQuery(IDb db, IMapper mapper, ILog log)
	{
		(Db, Errors, Log) = (db, new List<IMsg>(), log);

		Table = mapper.GetTableMapFor<TEntity>().Switch(
			some: x => x.Table,
			none: r => { Errors.Add(r); return new NullTable(); }
		);

		Parts = new QueryParts(Table);
	}

	/// <summary>
	/// Update <see cref="Parts"/> with a new value
	/// </summary>
	/// <param name="with">Function to perform the update</param>
	private IFluentQuery<TEntity, TId> Update(Func<QueryParts, QueryParts> with) =>
		this with { Parts = with(new(Parts)) };
}
