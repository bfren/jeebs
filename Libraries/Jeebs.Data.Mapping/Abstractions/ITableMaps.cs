using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Caches mappings of entities to tables
	/// </summary>
	public interface ITableMaps
	{
		/// <summary>
		/// Returns true if <typeparamref name="TEntity"/> has already been mapped
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		bool Exists<TEntity>();

		/// <summary>
		/// Add the TableMap
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="map">TableMap</param>
		bool TryAdd<TEntity>(TableMap map);

		/// <summary>
		/// Get map for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		TableMap GetMap<TEntity>();

		/// <summary>
		/// Get table name for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		string GetTableName<TEntity>();

		/// <summary>
		/// Get mapped columns for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		IEnumerable<IMappedColumn> GetColumns<TEntity>();

		/// <summary>
		/// Get Id Property for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		IMappedColumn GetIdProperty<TEntity>();

		/// <summary>
		/// Get Version Property for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		IMappedColumn? GetVersionProperty<TEntity>();
	}
}
