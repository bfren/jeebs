// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;
using Jeebs.Data.Enums;
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
	internal QueryParts QueryParts { get; private init; }

	/// <summary>
	/// Table that TEntity is mapped to
	/// </summary>
	internal ITable Table { get; private init; }

	/// <summary>
	/// List of added predicates
	/// </summary>
	internal IImmutableList<(string col, Compare cmp, dynamic val)> Predicates { get; init; } =
		new ImmutableList<(string, Compare, dynamic)>();

	/// <summary>
	/// List of added sort orders
	/// </summary>
	internal IImmutableList<(string col, SortOrder val)> Sorts { get; init; } =
		new ImmutableList<(string, SortOrder)>();

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="db">Database instance</param>
	/// <param name="log">ILog (should come with the context of the calling class)</param>
	internal FluentQuery(IDb db, ILog log)
	{
		(Db, Errors, Log) = (db, new List<IMsg>(), log);

		Table = Db.Client.Mapper.GetTableMapFor<TEntity>().Switch(
			some: x => x.Table,
			none: r => { Errors.Add(r); return new NullTable(); }
		);

		QueryParts = new QueryParts(Table);
	}
}
