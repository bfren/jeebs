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
		/// <param name="maps">[Optional] TableMaps - if null will use default static instance</param>
		public static void To<TTable>(TTable table, ITableMaps? maps = null)
			where TTable : Table
		{
			lock (_)
			{
				// Check TablesMap cache before doing anything
				if ((maps ?? TableMaps.Instance).Exists<TEntity>())
				{
					return;
				}

				// Validate table
				if (!validTables.Contains(typeof(TTable)))
				{
					Validate<TTable>();
				}

				// Get mapped properties and the corresponding column names
				var columns = from column in typeof(TTable).GetFields()
							  join property in typeof(TEntity).GetProperties() on column.Name equals property.Name
							  where property.GetCustomAttribute<IgnoreAttribute>() == null
							  select new MappedColumn
							  (
								  table: table.ToString(),
								  name: column.GetValue(table).ToString(),
								  property: property
							  );

				// Get ID property
				var idProperty = GetColumnWith<IdAttribute>(columns);

				// Create Table Map
				var map = new TableMap(table.ToString(), columns.ToList(), idProperty);

				// Get Version property
				if (typeof(IEntityWithVersion).IsAssignableFrom(typeof(TEntity)))
				{
					map.VersionColumn = GetColumnWith<VersionAttribute>(columns);
				}

				// Add Map
				(maps ?? TableMaps.Instance).TryAdd<TEntity>(map);

				// Get the column with the specified attribute
				static MappedColumn GetColumnWith<TAttribute>(IEnumerable<MappedColumn> columns)
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
			// Get the table field names
			var tableFieldNames = from f in typeof(TTable).GetFields()
								  select f.Name;

			// Get the entity property names
			var entityPropertyNames = from p in typeof(TEntity).GetProperties()
									  where p.GetCustomAttribute<IgnoreAttribute>() == null
									  select p.Name;

			// Compare the table columns with the entity properties
			var errors = new List<string>();

			// Check for missing table columns
			var missingTableFields = entityPropertyNames.Except(tableFieldNames);
			if (missingTableFields.Any())
			{
				foreach (var field in missingTableFields)
				{
					errors.Add($"The definition of table '{typeof(TTable).FullName}' is missing field '{field}'.");
				}
			}

			// Check for missing entity properties
			var missingEntityProperties = tableFieldNames.Except(entityPropertyNames);
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
				throw new Jx.Data.Mapping.InvalidTableMapException(string.Join("\n", errors));
			}

			// Add to the list of valid tables
			validTables.Add(typeof(TTable));
		}
	}
}
