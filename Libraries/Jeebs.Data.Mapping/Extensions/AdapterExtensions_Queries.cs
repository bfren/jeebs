using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
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
		/// <param name="adapter">IAdapter</param>
		public static string CreateSingleAndReturnId<T>(this IAdapter adapter)
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();

			// Get SQL from adapter
			return adapter.CreateSingleAndReturnId(map.Name, columns, aliases);
		}

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="adapter">IAdapter</param>
		public static string RetrieveSingleById<T>(this IAdapter adapter)
		{
			// Get map and select list
			var map = TableMaps.GetMap<T>();
			var select = new List<string>();

			// Add each column to the select list
			foreach (var mc in map.Columns)
			{
				select.Add(adapter.GetColumn(mc));
			}

			// Get SQL from adapter
			return adapter.RetrieveSingleById(select, map.Name, map.IdColumn.Column);
		}

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="adapter">IAdapter</param>
		public static string UpdateSingle<T>(this IAdapter adapter)
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();

			var id = map.IdColumn;
			var version = map.VersionColumn;

			// Get SQL from adapter
			if (version is MappedColumn v)
			{
				return adapter.UpdateSingle(map.Name, columns, aliases, id.Column, id.Property.Name, v.Column, v.Property.Name);
			}
			else
			{
				return adapter.UpdateSingle(map.Name, columns, aliases, id.Column, id.Property.Name);
			}
		}

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="adapter">IAdapter</param>
		public static string DeleteSingle<T>(this IAdapter adapter)
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();

			var id = map.IdColumn;
			var version = map.VersionColumn;

			// Get SQL from adapter
			if (version is MappedColumn v)
			{
				return adapter.DeleteSingle(map.Name, id.Column, id.Property.Name, v.Column, v.Property.Name);
			}
			else
			{
				return adapter.DeleteSingle(map.Name, id.Column, id.Property.Name);
			}
		}
	}
}
