using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// IAdapter extensions - queries
	/// </summary>
	public static partial class AdapterExtensions
	{
		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="maps">[Optional] IMapService - if null will use default static instance</param>
		public static string CreateSingleAndReturnId<T>(this IAdapter @this, IMapService? maps = null)
			where T : IEntity
		{
			// Get map and columns
			var map = (maps ?? MapService.Instance).GetTableMapFor<T>();
			(var columns, var aliases) = map.GetWriteableColumnNamesAndAliases();

			// Get SQL from adapter
			return @this.CreateSingleAndReturnId(map.Name, columns, aliases);
		}

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="maps">[Optional] IMapService - if null will use default static instance</param>
		public static string RetrieveSingleById<T>(this IAdapter @this, IMapService? maps = null)
			where T : IEntity
		{
			// Get map and select list
			var map = (maps ?? MapService.Instance).GetTableMapFor<T>();
			var select = new List<string>();

			// Add each column to the select list
			foreach (var mc in map.Columns)
			{
				select.Add(@this.GetColumn(mc));
			}

			// Get SQL from adapter
			return @this.RetrieveSingleById(map.Name, select, map.IdColumn.Name);
		}

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="maps">[Optional] IMapService - if null will use default static instance</param>
		public static string UpdateSingle<T>(this IAdapter @this, IMapService? maps = null)
			where T : IEntity
		{
			// Get map and columns
			var map = (maps ?? MapService.Instance).GetTableMapFor<T>();
			(var columns, var aliases) = map.GetWriteableColumnNamesAndAliases();
			var id = map.IdColumn;

			// Get SQL from adapter
			if (map.VersionColumn is MappedColumn v)
			{
				return @this.UpdateSingle(map.Name, columns, aliases, id.Name, id.Property.Name, v.Name, v.Property.Name);
			}
			else
			{
				return @this.UpdateSingle(map.Name, columns, aliases, id.Name, id.Property.Name);
			}
		}

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="maps">[Optional] IMapService - if null will use default static instance</param>
		public static string DeleteSingle<T>(this IAdapter @this, IMapService? maps = null)
			where T : IEntity
		{
			// Get map and columns
			var map = (maps ?? MapService.Instance).GetTableMapFor<T>();
			var id = map.IdColumn;

			// Get SQL from adapter
			if (map.VersionColumn is MappedColumn v)
			{
				return @this.DeleteSingle(map.Name, id.Name, id.Property.Name, v.Name, v.Property.Name);
			}
			else
			{
				return @this.DeleteSingle(map.Name, id.Name, id.Property.Name);
			}
		}
	}
}
