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
		public static string CreateSingleAndReturnId<T>(this IAdapter @this)
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();

			// Get SQL from adapter
			return @this.CreateSingleAndReturnId(map.Name, columns, aliases);
		}

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		public static string RetrieveSingleById<T>(this IAdapter @this)
		{
			// Get map and select list
			var map = TableMaps.GetMap<T>();
			var select = new List<string>();

			// Add each column to the select list
			foreach (var mc in map.Columns)
			{
				select.Add(@this.GetColumn(mc));
			}

			// Get SQL from adapter
			return @this.RetrieveSingleById(select, map.Name, map.IdColumn.Column);
		}

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		public static string UpdateSingle<T>(this IAdapter @this)
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();
			var id = map.IdColumn;

			// Get SQL from adapter
			if (map.VersionColumn is MappedColumn v)
			{
				return @this.UpdateSingle(map.Name, columns, aliases, id.Column, id.Property.Name, v.Column, v.Property.Name);
			}
			else
			{
				return @this.UpdateSingle(map.Name, columns, aliases, id.Column, id.Property.Name);
			}
		}

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		public static string DeleteSingle<T>(this IAdapter @this)
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();
			var id = map.IdColumn;

			// Get SQL from adapter
			if (map.VersionColumn is MappedColumn v)
			{
				return @this.DeleteSingle(map.Name, id.Column, id.Property.Name, v.Column, v.Property.Name);
			}
			else
			{
				return @this.DeleteSingle(map.Name, id.Column, id.Property.Name);
			}
		}
	}
}
