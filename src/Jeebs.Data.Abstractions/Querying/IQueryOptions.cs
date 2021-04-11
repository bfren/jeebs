﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// An alternative method to <see cref="IQueryParts"/> for building a database query
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">Entity ID type</typeparam>
	public interface IQueryOptions<TEntity, TId>
		where TEntity : IWithId<TId>
		where TId : StrongId
	{
		/// <summary>
		/// Query Id
		/// </summary>
		TId? Id { get; init; }

		/// <summary>
		/// Query IDs
		/// </summary>
		TId[]? Ids { get; init; }

		/// <inheritdoc cref="IQueryParts.Sort"/>
		(IColumn column, SortOrder order)[]? Sort { get; init; }

		/// <inheritdoc cref="IQueryParts.SortRandom"/>
		bool SortRandom { get; init; }

		/// <inheritdoc cref="IQueryParts.Maximum"/>
		long? Maximum { get; init; }

		/// <inheritdoc cref="IQueryParts.Skip"/>
		long Skip { get; init; }

		/// <summary>
		/// Convert the query options to <see cref="IQueryParts"/> for use in a database query
		/// </summary>
		/// <typeparam name="TModel">Model type to use for selecting columns</typeparam>
		Option<IQueryParts> GetParts<TModel>();
	}
}
