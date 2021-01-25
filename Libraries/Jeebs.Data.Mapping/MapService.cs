using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Jx.Data.Mapping;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc/>
	public sealed class MapService : IMapService
	{
		#region Static

		/// <summary>
		/// Default (global) instance
		/// </summary>
		internal static IMapService Instance =>
			instance.Value;

		/// <summary>
		/// Lazily create a <see cref="MapService"/>
		/// </summary>
		private static readonly Lazy<IMapService> instance = new Lazy<IMapService>(() => new MapService(), true);

		#endregion

		/// <summary>
		/// Mapped entities
		/// </summary>
		private readonly ConcurrentDictionary<Type, TableMap> mappedEntities = new ConcurrentDictionary<Type, TableMap>();

		/// <summary>
		/// Only allow internal creation
		/// </summary>
		internal MapService() { }

		/// <summary>
		/// FOR TESTING
		/// Map the specified <typeparamref name="TEntity"/> to the specified <typeparamref name="TTable"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TTable">Table type</typeparam>
		internal TableMap Map<TEntity, TTable>()
			where TEntity : IEntity
			where TTable : Table, new() =>
			Map<TEntity>(new TTable());

		/// <inheritdoc/>
		public TableMap Map<TEntity>(Table table)
			where TEntity : IEntity =>
			mappedEntities.GetOrAdd(typeof(TEntity), _ =>
			{
				// Validate table
				var (valid, errors) = ValidateTable<TEntity>(table);
				if (!valid)
				{
					throw new InvalidTableMapException(errors);
				}

				// Get mapped columns
				var columns = GetMappedColumns<TEntity>(table);

				// Get ID property
				var idProperty = GetColumnWithAttribute<TEntity, IdAttribute>(columns);

				// Create Table Map
				var map = new TableMap(table.ToString(), columns, idProperty);

				// Get Version property
				if (typeof(IEntityWithVersion).IsAssignableFrom(typeof(TEntity)))
				{
					map.VersionColumn = GetColumnWithAttribute<TEntity, VersionAttribute>(columns);
				}

				// Return map
				return map;
			});

		/// <summary>
		/// FOR TESTING
		/// Map the specified <typeparamref name="TEntity"/> to the specified <typeparamref name="TTable"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TTable">Table type</typeparam>
		internal (bool valid, string errors) ValidateTable<TEntity, TTable>()
			where TEntity : IEntity
			where TTable : Table, new() =>
			ValidateTable<TEntity>(new TTable());

		/// <inheritdoc/>
		public (bool valid, string errors) ValidateTable<TEntity>(Table table)
			where TEntity : IEntity
		{
			// Get table type
			var tableType = table.GetType();

			// Get the table field names
			var tablePropertyNames = from p in tableType.GetProperties()
									 where p.PropertyType.IsPublic
									 select p.Name;

			// Get the entity property names
			var entityPropertyNames = from p in typeof(TEntity).GetProperties()
									  where p.GetCustomAttribute<IgnoreAttribute>() == null
									  select p.Name;

			// Compare the table columns with the entity properties
			var errors = new List<string>();

			// Check for missing table columns
			var missingTableFields = entityPropertyNames.Except(tablePropertyNames);
			if (missingTableFields.Any())
			{
				foreach (var field in missingTableFields)
				{
					errors.Add($"The definition of table '{tableType.FullName}' is missing field '{field}'.");
				}
			}

			// Check for missing entity properties
			var missingEntityProperties = tablePropertyNames.Except(entityPropertyNames);
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
				return (false, string.Join("\n", errors));
			}

			// Add to the list of valid tables
			return (true, string.Empty);
		}

		/// <summary>
		/// FOR TESTING
		/// Get mapped columns from the specified <typeparamref name="TEntity"/> and <typeparamref name="TTable"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TTable">Table type</typeparam>
		internal IMappedColumnList GetMappedColumns<TEntity, TTable>()
			where TEntity : IEntity
			where TTable : Table, new() =>
			GetMappedColumns<TEntity>(new TTable());

		/// <inheritdoc/>
		public IMappedColumnList GetMappedColumns<TEntity>(Table table)
			where TEntity : IEntity
		{
			// Get non-ignored columns
			var columns = from column in table.GetType().GetProperties()
						  let columnName = column.GetValue(table)?.ToString()
						  join property in typeof(TEntity).GetProperties() on column.Name equals property.Name
						  where property.GetCustomAttribute<IgnoreAttribute>() == null
						  select new MappedColumn
						  (
							  table: table.ToString(),
							  name: columnName,
							  property: property
						  );

			// Return as list
			return new MappedColumnList(columns);
		}

		/// <inheritdoc/>
		public IMappedColumn GetColumnWithAttribute<TEntity, TAttribute>(IMappedColumnList columns)
			where TEntity : IEntity
			where TAttribute : Attribute
		{
			var attr = typeof(TAttribute);
			var name = attr.Name.Replace(nameof(Attribute), string.Empty);
			var cols = columns.Where(p => p.Property.GetCustomAttribute(attr) != null);

			return cols.Count() switch
			{
				1 =>
					cols.Single(),

				0 =>
					throw new MissingAttributeException(typeof(TEntity), name),

				_ =>
					throw new MultipleAttributesException(typeof(TEntity), name)
			};
		}

		/// <inheritdoc/>
		public TableMap GetTableMapFor<TEntity>()
			where TEntity : IEntity
		{
			if (mappedEntities.TryGetValue(typeof(TEntity), out var map))
			{
				return map;
			}

			throw new UnmappedEntityException(typeof(TEntity));
		}

		/// <summary>
		/// Clear caches
		/// </summary>
		public void Dispose() =>
			mappedEntities.Clear();
	}
}
