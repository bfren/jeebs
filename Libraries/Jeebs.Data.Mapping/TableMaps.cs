using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Holds TableMaps of mapped entities
	/// </summary>
	public sealed class TableMaps
	{
		#region Static 

		/// <summary>
		/// TableMaps default instance
		/// </summary>
		public static readonly TableMaps Instance = new TableMaps();

		#endregion

		private readonly ConcurrentDictionary<Type, TableMap> maps = new ConcurrentDictionary<Type, TableMap>();

		/// <summary>
		/// Only allow internal construction
		/// </summary>
		internal TableMaps() { }

		/// <summary>
		/// Returns true if <typeparamref name="TEntity"/> has already been mapped
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public bool Exists<TEntity>()
			=> maps.ContainsKey(typeof(TEntity));

		/// <summary>
		/// Add the TableMap
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <exception cref="Jx.Data.MappingException">If <typeparamref name="TEntity"/> has already been mapped</exception>
		/// <param name="map">TableMap</param>
		internal bool TryAdd<TEntity>(TableMap map)
			=> maps.TryAdd(typeof(TEntity), map);

		/// <summary>
		/// Safely get a value from a mapped entity
		/// </summary>
		/// <typeparam name="TReturn">Return type</typeparam>
		/// <param name="type">Entity type</param>
		/// <param name="expression">Expression to get the mapped value</param>
		private TReturn SafeGet<TReturn>(Type type, Expression<Func<TableMap, TReturn>> expression)
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
		public TableMap GetMap<TEntity>()
			=> SafeGet(typeof(TEntity), map => map);

		/// <summary>
		/// Get table name for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public string GetTableName<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.Name);

		/// <summary>
		/// Get mapped columns for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public IEnumerable<MappedColumn> GetColumns<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.Columns);

		/// <summary>
		/// Get Id Property for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public MappedColumn GetIdProperty<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.IdColumn);

		/// <summary>
		/// Get Version Property for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public MappedColumn? GetVersionProperty<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.VersionColumn);
	}
}
