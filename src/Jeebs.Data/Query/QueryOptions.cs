// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using StrongId;

namespace Jeebs.Data.Query;

/// <inheritdoc cref="IQueryOptions{TId}"/>
public abstract record class QueryOptions<TId> : IQueryOptions<TId>
	where TId : class, IStrongId, new()
{
	/// <summary>
	/// Abstraction for building query parts
	/// </summary>
	protected IQueryPartsBuilder<TId> Builder { get; init; }

	/// <inheritdoc/>
	public TId? Id { get; init; }

	/// <inheritdoc/>
	public IImmutableList<TId> Ids { get; init; } =
		new ImmutableList<TId>();

	/// <inheritdoc/>
	public IImmutableList<(IColumn column, SortOrder order)> Sort { get; init; } =
		new ImmutableList<(IColumn column, SortOrder order)>();

	/// <inheritdoc/>
	public bool SortRandom { get; init; }

	/// <inheritdoc/>
	public ulong? Maximum { get; init; } = 10;

	/// <inheritdoc/>
	public ulong Skip { get; init; }

	/// <summary>
	/// Inject builder
	/// </summary>
	/// <param name="builder">IQueryPartsBuilder</param>
	protected QueryOptions(IQueryPartsBuilder<TId> builder) =>
		Builder = builder;

	/// <inheritdoc/>
	public Maybe<IQueryParts> ToParts<TModel>() =>
		Build(
			Builder.Create<TModel>(Maximum, Skip)
		)
		.Map(
			x => (IQueryParts)x,
			F.DefaultHandler
		);

	/// <summary>
	/// Build QueryParts
	/// </summary>
	/// <param name="parts">Initial QueryParts</param>
	protected virtual Maybe<QueryParts> Build(Maybe<QueryParts> parts) =>
		parts.SwitchIf(
			_ => Id is not null || Ids.Count > 0,
			x => Builder.AddWhereId(x, Id, Ids)
		)
		.SwitchIf(
			_ => SortRandom || Sort.Count > 0,
			x => Builder.AddSort(x, SortRandom, Sort)
		);
}
