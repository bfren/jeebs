// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using static F.OptionF;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryOptions{TId}"/>
	public abstract record QueryOptions<TId> : IQueryOptions<TId>
		where TId : StrongId
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
		public long? Maximum { get; init; } = 10;

		/// <inheritdoc/>
		public long Skip { get; init; }

		/// <summary>
		/// Inject builder
		/// </summary>
		/// <param name="builder">IQueryPartsBuilder</param>
		protected QueryOptions(IQueryPartsBuilder<TId> builder) =>
			Builder = builder;

		/// <inheritdoc/>
		public Option<IQueryParts> ToParts<TModel>() =>
			Build(
				Builder.Create<TModel>(Maximum, Skip)
			)
			.Map(
				x => (IQueryParts)x,
				DefaultHandler
			);

		/// <summary>
		/// Build QueryParts
		/// </summary>
		/// <param name="parts">Initial QueryParts</param>
		protected virtual Option<QueryParts> Build(Option<QueryParts> parts) =>
			parts.SwitchIf(
				_ => Id?.Value > 0 || Ids.Count > 0,
				x => Builder.AddWhereId(x, Id, Ids)
			)
			.SwitchIf(
				_ => SortRandom || Sort.Count > 0,
				x => Builder.AddSort(x, SortRandom, Sort)
			);
	}
}
