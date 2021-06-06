// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;

namespace Jeebs.Data
{
	/// <summary>
	/// Map an entity to a table in a fluent style
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public static class Map<TEntity>
		where TEntity : IWithId
	{
		/// <inheritdoc/>
		public static ITableMap To<TTable>()
			where TTable : Table, new() =>
			To<TTable>(Mapper.Instance);

		/// <summary>
		/// Map entity to the specified table type
		/// </summary>
		/// <param name="mapper">IMapper</param>
		internal static ITableMap To<TTable>(IMapper mapper)
			where TTable : Table, new() =>
			mapper.Map<TEntity>(new TTable());

		/// <summary>
		/// Map entity to the specified table
		/// </summary>
		/// <param name="table">The table to map <typeparamref name="TEntity"/> to</param>
		public static ITableMap To<TTable>(TTable table)
			where TTable : Table =>
			To(table, Mapper.Instance);

		/// <summary>
		/// Map entity to the specified table
		/// </summary>
		/// <param name="table">The table to map <typeparamref name="TEntity"/> to</param>
		/// <param name="mapper">IMapper</param>
		internal static ITableMap To<TTable>(TTable table, IMapper mapper)
			where TTable : Table =>
			mapper.Map<TEntity>(table);
	}
}
