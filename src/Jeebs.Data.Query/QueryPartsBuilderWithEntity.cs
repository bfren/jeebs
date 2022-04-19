// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Map;
using Jeebs.Messages;
using StrongId;

namespace Jeebs.Data.Query;

/// <summary>
/// Builds a <see cref="QueryParts"/> object from various options
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TId">Entity ID type</typeparam>
public abstract class QueryPartsBuilderWithEntity<TEntity, TId> : QueryPartsBuilder<TId>
	where TEntity : IWithId<TId>
	where TId : class, IStrongId, new()
{
	private readonly IEntityMapper mapper;

	/// <summary>
	/// Get table map for <typeparamref name="TEntity"/>
	/// </summary>
	public virtual Lazy<ITableMap> Map =>
		new(() =>
			mapper.GetTableMapFor<TEntity>().Unwrap(r =>
				throw Msg.CreateException(r)
			)
		);

	/// <inheritdoc/>
	public override ITable Table =>
		Map.Value.Table;

	/// <inheritdoc/>
	public override IColumn IdColumn =>
		Map.Value.IdColumn;

	/// <summary>
	/// Create with default mapper
	/// </summary>
	protected QueryPartsBuilderWithEntity() : this(EntityMapper.Instance) { }

	/// <summary>
	/// Inject mapper
	/// </summary>
	/// <param name="mapper">IMapper</param>
	protected QueryPartsBuilderWithEntity(IEntityMapper mapper) =>
		this.mapper = mapper;
}
