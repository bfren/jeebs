using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Holds TableMaps of mapped entities
	/// </summary>
	public static class TableMaps
	{
		/// <summary>
		/// Mapped entities
		/// </summary>
		private static readonly ConcurrentDictionary<Type, TableMap> maps;

		/// <summary>
		/// Create object
		/// </summary>
		static TableMaps()
			=> maps = new ConcurrentDictionary<Type, TableMap>();

		/// <summary>
		/// Returns true if <typeparamref name="TEntity"/> has already been mapped
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public static bool Exists<TEntity>()
			=> maps.ContainsKey(typeof(TEntity));

		/// <summary>
		/// Add the TableMap
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <exception cref="Jx.Data.MappingException">If <typeparamref name="TEntity"/> has already been mapped</exception>
		/// <param name="map">TableMap</param>
		internal static void Add<TEntity>(TableMap map)
		{
			// Don't map the same entity twice
			if (Exists<TEntity>())
			{
				throw new Jx.Data.MappingException($"Entity {typeof(TEntity).FullName} has already been mapped.");
			}

			// Add the table map
			maps.TryAdd(typeof(TEntity), map);
		}

		/// <summary>
		/// Safely get a value from a mapped entity
		/// </summary>
		/// <typeparam name="TReturn">Return type</typeparam>
		/// <param name="type">Entity type</param>
		/// <param name="expression">Expression to get the mapped value</param>
		private static TReturn SafeGet<TReturn>(Type type, Expression<Func<TableMap, TReturn>> expression)
		{
			if (maps.TryGetValue(type, out var map))
			{
				return expression.Compile().Invoke(map);
			}

			throw new Jx.Data.MappingException($"Entity {type} has not been mapped.");
		}

		/// <summary>
		/// Get map for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public static TableMap GetMap<TEntity>()
			=> SafeGet(typeof(TEntity), map => map);

		/// <summary>
		/// Get table name for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public static string GetTableName<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.Name);

		/// <summary>
		/// Get mapped columns for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public static IEnumerable<MappedColumn> GetColumns<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.Columns);

		/// <summary>
		/// Get Id Property for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public static MappedColumn GetIdProperty<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.IdColumn);

		/// <summary>
		/// Get Version Property for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public static MappedColumn? GetVersionProperty<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.VersionColumn);
	}
}
