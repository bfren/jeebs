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
		/// <returns>SQL query</returns>
		public override string CreateSingleAndReturnId<T>()
		{
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();

			return $@"
					INSERT INTO {map.Name} ({string.Join(", ", columns)})
					VALUES (@{string.Join(", @", aliases)});
					SELECT LAST_INSERT_ID();
				";
		}

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>SQL query</returns>
		public override string RetrieveSingleById<T>(int id)
		{
			var map = TableMaps.GetMap<T>();
			var select = new List<string>();

			foreach (var mc in map.Columns)
			{
				select.Add($"{mc.Column} AS '{mc.Property.Name}'");
			}

			return $"SELECT {string.Join(", ", select)} FROM {map.Name} WHERE {map.IdColumn.Column} = '{id}';";
		}

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Id value</param>
		/// <param name="version">[Optional] Version</param>
		/// <returns>SQL query</returns>
		public override string UpdateSingle<T>(int id, long? version = null)
		{
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();

			var update = new List<string>();
			for (int i = 0; i < columns.Count; i++)
			{
				update.Add($"{columns[i]} = @{aliases[i]}");
			}

			var sql = $@"
					UPDATE {map.Name}
					SET {string.Join(", ", update)}
					WHERE {map.IdColumn.Column} = '{id}'
				";

			if (map.VersionColumn is MappedColumn versionColumn && version is long versionValue)
			{
				sql += $" AND {versionColumn} = '{versionValue}'";
			}

			return $"{sql};";
		}

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Id value</param>
		/// <param name="version">[Optional] Version</param>
		/// <returns>SQL query</returns>
		public override string DeleteSingle<T>(int id, long? version = null)
		{
			var map = TableMaps.GetMap<T>();
			var sql = $"DELETE {map.Name} WHERE {map.IdColumn.Column} = '{id}'";

			if (map.VersionColumn is MappedColumn versionColumn && version is long versionValue)
			{
				sql += $" AND {versionColumn} = '{versionValue}'";
			}

			return $"{sql};";
		}
	}
}