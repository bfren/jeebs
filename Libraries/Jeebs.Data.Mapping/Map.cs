using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Map an entity to a table
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public static class Map<TEntity>
		where TEntity : IEntity
	{
		/// <summary>
		/// Provides thread-safe locking
		/// </summary>
		private static readonly object _ = new object();

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
			lock (_)
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
				var id = GetPropertyWith<IdAttribute>(columns);

				// Create Table Map
				var map = new TableMap(adapter.Escape(table.ToString()), columns.ToList(), id);

				// Get Version property
				if (typeof(IEntityWithVersion).IsAssignableFrom(typeof(TEntity)))
				{
					map.VersionColumn = GetPropertyWith<VersionAttribute>(columns);
				}

				// Add Map
				TableMaps.TryAdd<TEntity>(map);

				// Get the property with the specified attribute
				static MappedColumn GetPropertyWith<TAttribute>(IEnumerable<MappedColumn> columns)
					where TAttribute : Attribute
				{
					var name = typeof(TAttribute).Name.Replace(nameof(Attribute), string.Empty);
					var cols = columns.Where(p => p.Property.GetCustomAttribute<TAttribute>() != null).ToList();
					return cols.Count switch
					{
						1 => cols.Single(),
						0 => throw new Jx.Data.Mapping.MissingAttributeException(typeof(TEntity), name),
						_ => throw new Jx.Data.Mapping.MultipleAttributesException(typeof(TEntity), name)
					};
				}
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
