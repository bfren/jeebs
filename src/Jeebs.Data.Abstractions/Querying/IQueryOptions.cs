// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// An alternative method to <see cref="IQueryParts"/> for building a database query
	/// </summary>
	/// <typeparam name="TId">Entity ID type</typeparam>
	public interface IQueryOptions<TId>
		where TId : IStrongId
	{
		/// <summary>
		/// Query Id
		/// </summary>
		TId? Id { get; init; }

		/// <summary>
		/// Query IDs
		/// </summary>
		IImmutableList<TId> Ids { get; init; }

		/// <inheritdoc cref="IQueryParts.Sort"/>
		IImmutableList<(IColumn column, SortOrder order)> Sort { get; init; }

		/// <inheritdoc cref="IQueryParts.SortRandom"/>
		bool SortRandom { get; init; }

		/// <inheritdoc cref="IQueryParts.Maximum"/>
		ulong? Maximum { get; init; }

		/// <inheritdoc cref="IQueryParts.Skip"/>
		ulong Skip { get; init; }

		/// <summary>
		/// Convert the query options to <see cref="IQueryParts"/> for use in a database query
		/// </summary>
		/// <typeparam name="TModel">Model type to use for selecting columns</typeparam>
		Option<IQueryParts> ToParts<TModel>();
	}
}
