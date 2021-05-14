// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Builds a <see cref="QueryParts"/> object from various options
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">Entity ID type</typeparam>
	public abstract record QueryPartsBuilderWithEntity<TEntity, TId> : QueryPartsBuilder<TId>
		where TEntity : IWithId<TId>
		where TId : StrongId
	{
		private readonly IMapper mapper;

		public virtual Lazy<ITableMap> Map =>
			new(() =>
				mapper.GetTableMapFor<TEntity>().Unwrap(r =>
					throw MsgException.Create(r)
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
		protected QueryPartsBuilderWithEntity() =>
			mapper = Mapper.Instance;
	}
}
