// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Mapping
{
	/// <summary>
	/// Map an entity to a table
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public static class Map<TEntity>
		where TEntity : IEntity
	{
		/// <summary>
		/// Map entity to the specified table type
		/// </summary>
		public static ITableMap To<TTable>()
			where TTable : Table, new() =>
			To<TTable>(MapService.Instance);

		/// <summary>
		/// Map entity to the specified table type
		/// </summary>
		/// <param name="mapService">IMapServicee</param>
		internal static ITableMap To<TTable>(IMapService mapService)
			where TTable : Table, new() =>
			mapService.Map<TEntity>(new TTable());

		/// <summary>
		/// Map entity to the specified table
		/// </summary>
		/// <param name="table">The table to map <typeparamref name="TEntity"/> to</param>
		public static ITableMap To<TTable>(TTable table)
			where TTable : Table =>
			To(table, MapService.Instance);

		/// <summary>
		/// Map entity to the specified table
		/// </summary>
		/// <param name="table">The table to map <typeparamref name="TEntity"/> to</param>
		/// <param name="mapService">IMapService</param>
		internal static ITableMap To<TTable>(TTable table, IMapService mapService)
			where TTable : Table =>
			mapService.Map<TEntity>(table);
	}
}
