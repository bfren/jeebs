// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Map;

namespace Jeebs.Data.QueryBuilder;

/// <summary>
/// Builds a <see cref="QueryParts"/> object from various options.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TId">Entity ID type.</typeparam>
/// <remarks>
/// Inject mapper.
/// </remarks>
/// <param name="mapper">IEntityMapper.</param>
public abstract class QueryPartsBuilderWithEntity<TEntity, TId>(IEntityMapper mapper) : QueryPartsBuilder<TId>
	where TEntity : IWithId
	where TId : class, IUnion, new()
{
	private readonly IEntityMapper mapper = mapper;

	/// <summary>
	/// Get table map for <typeparamref name="TEntity"/>.
	/// </summary>
	public virtual Lazy<ITableMap> Map =>
		new(() => mapper.GetTableMapFor<TEntity>().Unwrap());

	/// <inheritdoc/>
	public override ITable Table =>
		Map.Value.Table;

	/// <inheritdoc/>
	public override IColumn IdColumn =>
		Map.Value.IdColumn;

	/// <summary>
	/// Create with default mapper.
	/// </summary>
	protected QueryPartsBuilderWithEntity() : this(EntityMapper.Instance) { }
}
