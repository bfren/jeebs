using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc/>
	public sealed class TableMaps : ITableMaps
	{
		#region Static 

		/// <summary>
		/// TableMaps default instance
		/// </summary>
		public static readonly ITableMaps Instance = new TableMaps();

		#endregion

		private readonly ConcurrentDictionary<Type, TableMap> maps = new ConcurrentDictionary<Type, TableMap>();

		/// <summary>
		/// Only allow internal construction
		/// </summary>
		internal TableMaps() { }

		/// <inheritdoc/>
		public bool Exists<TEntity>()
			=> maps.ContainsKey(typeof(TEntity));

		/// <summary>
		/// Add the TableMap
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <exception cref="Jx.Data.MappingException">If <typeparamref name="TEntity"/> has already been mapped</exception>
		/// <param name="map">TableMap</param>
		public bool TryAdd<TEntity>(TableMap map)
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

			throw new Jx.Data.Mapping.UnmappedEntityException(type);
		}

		/// <inheritdoc/>
		public TableMap GetMap<TEntity>()
			=> SafeGet(typeof(TEntity), map => map);

		/// <inheritdoc/>
		public string GetTableName<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.Name);

		/// <inheritdoc/>
		public IEnumerable<IMappedColumn> GetColumns<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.Columns);

		/// <inheritdoc/>
		public IMappedColumn GetIdProperty<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.IdColumn);

		/// <inheritdoc/>
		public IMappedColumn? GetVersionProperty<TEntity>()
			=> SafeGet(typeof(TEntity), map => map.VersionColumn);
	}
}
