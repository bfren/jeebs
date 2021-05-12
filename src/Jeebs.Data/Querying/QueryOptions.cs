// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Linq;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Linq;

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
		public virtual Option<IQueryParts> ToParts<TModel>() =>
			from map in GetMap()
			from cols in GetColumns<TModel>(map.table)
			from parts in BuildParts(map.table, cols, map.idColumn)
			select (IQueryParts)parts;

		/// <summary>
		/// Get mapped Table and ID Column
		/// </summary>
		protected abstract Option<(ITable table, IColumn idColumn)> GetMap();

		/// <summary>
		/// Get select columns for the specified <typeparamref name="TModel"/>
		/// </summary>
		/// <typeparam name="TModel">Model type to use for selecting columns</typeparam>
		/// <param name="table">Select columns from this table</param>
		protected virtual Option<IColumnList> GetColumns<TModel>(ITable table) =>
			Extract<TModel>.From(table);

		/// <summary>
		/// Actually get the various query parts using helper functions
		/// </summary>
		/// <param name="table">Primary table</param>
		/// <param name="cols">Select ColumnList</param>
		/// <param name="idColumn">ID Column</param>
		protected virtual Option<QueryParts> BuildParts(ITable table, IColumnList cols, IColumn idColumn) =>
			Builder.Create(
				table, cols, Maximum, Skip
			)
			.SwitchIf(
				_ => Id?.Value > 0 || Ids.Count > 0,
				x => Builder.AddWhereId(x, idColumn, Id, Ids)
			)
			.SwitchIf(
				_ => SortRandom || Sort.Count > 0,
				x => Builder.AddSort(x, SortRandom, Sort)
			);

		#region Testing

		internal Option<IColumnList> GetColumnsTest<TModel>(ITable table) =>
			GetColumns<TModel>(table);

		internal Option<QueryParts> BuildPartsTest(ITable table, IColumnList cols, IColumn idColumn) =>
			BuildParts(table, cols, idColumn);

		#endregion
	}

	/// <inheritdoc cref="IQueryOptions{TId}"/>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">Entity ID type</typeparam>
	public abstract record QueryOptions<TEntity, TId> : QueryOptions<TId>, IQueryOptions<TId>
		where TEntity : IWithId<TId>
		where TId : StrongId
	{
		private readonly IMapper mapper;

		/// <summary>
		/// Create using default <see cref="IMapper"/>
		/// </summary>
		/// <param name="builder">IQueryPartsBuilder</param>
		protected QueryOptions(IQueryPartsBuilder<TId> builder) : this(Mapper.Instance, builder) { }

		/// <summary>
		/// Inject an <see cref="IMapper"/> and <see cref="TBuilder"/> for testing
		/// </summary>
		/// <param name="mapper">IMapper</param>
		/// <param name="builder">IQueryPartsBuilder</param>
		internal QueryOptions(IMapper mapper, IQueryPartsBuilder<TId> builder) : base(builder) =>
			this.mapper = mapper;

		/// <inheritdoc/>
		protected override Option<(ITable table, IColumn idColumn)> GetMap() =>
			from map in mapper.GetTableMapFor<TEntity>()
			select (map.Table, (IColumn)map.IdColumn);

		#region Testing

		internal Option<(ITable table, IColumn idColumn)> GetMapTest() =>
			GetMap();

		#endregion
	}
}
