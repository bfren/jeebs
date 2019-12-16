using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs.Data.Clients.MySql
{
	/// <summary>
	/// MySql adapter
	/// </summary>
	public sealed class MySqlAdapter : Adapter
	{
		/// <summary>
		/// Create object
		/// </summary>
		public MySqlAdapter() : base('.', '`', '`') { }

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public override string CreateSingleAndReturnId<T>()
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();

			// Return SQL
			return $"INSERT INTO {map.Name} ({string.Join(", ", columns)}) " +
				$"VALUES (@{string.Join(", @", aliases)}); " +
				$"SELECT LAST_INSERT_ID();";
		}

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public override string RetrieveSingleById<T>()
		{
			// Get map and select list
			var map = TableMaps.GetMap<T>();
			var select = new List<string>();

			// Add each column to the select list
			foreach (var mc in map.Columns)
			{
				select.Add($"{mc} AS '{mc.Property.Name}'");
			}

			// Return SQL
			return $"SELECT {string.Join(", ", select)} FROM {map} WHERE {map.IdColumn} = @id;";
		}

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public override string UpdateSingle<T>()
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();

			// Add each column to the update list
			var update = new List<string>();
			for (int i = 0; i < columns.Count; i++)
			{
				update.Add($"{columns[i]} = @{aliases[i]}");
			}

			// Build SQL
			var sql = $"UPDATE {map} " +
				$"SET {string.Join(", ", update)} " +
				$"WHERE {map.IdColumn} = @{map.IdColumn.Property.Name}";

			// Add Version column
			if (map.VersionColumn is MappedColumn versionColumn)
			{
				sql += $" AND {versionColumn} = @{versionColumn.Property.Name} - 1";
			}

			// Return SQL
			return $"{sql};";
		}

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public override string DeleteSingle<T>()
		{
			// Get map
			var map = TableMaps.GetMap<T>();

			// Build SQL
			var sql = $"DELETE FROM {map} WHERE {map.IdColumn} = @{map.IdColumn.Property.Name}";

			// Add Version column
			if (map.VersionColumn is MappedColumn versionColumn)
			{
				sql += $" AND {versionColumn} = @{versionColumn.Property.Name}";
			}

			// Return SQL
			return $"{sql};";
		}
	}
}