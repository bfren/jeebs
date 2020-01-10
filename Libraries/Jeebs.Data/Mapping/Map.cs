using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Map an entity to a table
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public static class Map<TEntity>
		where TEntity : IEntity
	{
		/// <summary>
		/// Valid tables
		/// </summary>
		private static readonly ConcurrentBag<Type> validTables = new ConcurrentBag<Type>();

		/// <summary>
		/// Map entity to the specified table
		/// </summary>
		/// <param name="table">Table to map TEntity to</param>
		/// <param name="adapter">IAdapter</param>
		public static void To<TTable>(TTable table, IAdapter adapter)
			where TTable : Table
		{
			// Check TablesMap cache before doing anything
			if (TableMaps.Exists<TEntity>())
			{
				return;
			}

			// Validate table
			if (!validTables.Contains(typeof(TTable)))
			{
				Validate<TTable>();
				validTables.Add(typeof(TTable));
			}

			// Get mapped properties and the corresponding column names
			var columns = from column in typeof(TTable).GetFields()
						  join property in typeof(TEntity).GetProperties() on column.Name equals property.Name
						  where property.GetCustomAttribute<IgnoreAttribute>() == null
						  select new MappedColumn
						  (
							  column: adapter.Escape(column.GetValue(table).ToString()),
							  property: property
						  );

			// Get ID property
			var id = GetPropertyWith<IdAttribute>("Id");

			// Create Table Map
			var map = new TableMap(adapter.Escape(table.ToString()), columns.ToList(), id);

			// Get Version property
			if (typeof(IEntityWithVersion).IsAssignableFrom(typeof(TEntity)))
			{
				map.VersionColumn = GetPropertyWith<VersionAttribute>("Version");
			}

			// Add Map
			TableMaps.Add<TEntity>(map);

			// Get the property with the specified attribute
			MappedColumn GetPropertyWith<TAttribute>(string attr)
				where TAttribute : Attribute
			{
				var pi = columns.Where(p => p.Property.GetCustomAttribute<TAttribute>() != null);
				if (!pi.Any())
				{
					throw new Jx.Data.MappingException($"You must define [{attr}] property for entity type '{typeof(TEntity)}'.");
				}
				else if (pi.Count() > 1)
				{
					throw new Jx.Data.MappingException($"There is more than one [{attr}] property defined in '{typeof(TEntity)}'.");
				}

				return pi.Single();
			}
		}

		/// <summary>
		/// Validate a table's columns against the entity's properties
		/// </summary>
		/// <typeparam name="TTable">Type of table to validate</typeparam>
		/// <exception cref="Jx.Data.MappingException">If a column is missing from the table properties, or a property is missing from the entity properties</exception>
		private static void Validate<TTable>()
			where TTable : Table
		{
			// Get the table columns
			var tableColumns = typeof(TTable)
				.GetFields()
				.Select(p => p.Name);

			// Get the entity properties
			var entityProperties = typeof(TEntity)
				.GetProperties()
				.Where(p => p.GetCustomAttribute<IgnoreAttribute>() == null)
				.Select(p => p.Name);

			// Compare the table columns with the entity properties
			var errors = new List<string>();

			// Check for missing table columns
			var missingTableColumns = entityProperties.Except(tableColumns);
			if (missingTableColumns.Any())
			{
				foreach (var column in missingTableColumns)
				{
					errors.Add($"The definition of table '{typeof(TTable).FullName}' is missing column '{column}'.");
				}
			}

			// Check for missing entity properties
			var missingEntityProperties = tableColumns.Except(entityProperties);
			if (missingEntityProperties.Any())
			{
				foreach (var property in missingEntityProperties)
				{
					errors.Add($"The definition of entity '{typeof(TEntity).FullName}' is missing property '{property}'.");
				}
			}

			// If there are any errors, throw a MappingException
			if (errors.Count > 0)
			{
				throw new Jx.Data.MappingException(string.Join("\n", errors));
			}
		}
	}
}
