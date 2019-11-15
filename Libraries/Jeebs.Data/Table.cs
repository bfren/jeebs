using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Table
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public abstract class Table<TEntity>
	{
		/// <summary>
		/// Adapter
		/// </summary>
		protected readonly IAdapter adapter;

		/// <summary>
		/// Table name
		/// </summary>
		private readonly string name;

		/// <summary>
		/// Whether or not to use versioning
		/// </summary>
		private readonly bool useVersion;

		/// <summary>
		/// Whether or not the table has been validated
		/// </summary>
		private bool valid = false;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="adapter">Adapter</param>
		/// <param name="name">Table name</param>
		/// <param name="useVersion">Whether or not to use row versioning</param>
		protected Table(in IAdapter adapter, in string name, bool useVersion = false)
		{
			this.adapter = adapter;
			this.name = name;
			this.useVersion = useVersion;

			Validate();
			Map();
		}

		/// <summary>
		/// Validate the table's columns against the entity's properties
		/// </summary>
		/// <exception cref="Jx.Data.MappingException">If a column is missing from the table properties, or a property is missing from the entity properties</exception>
		private void Validate()
		{
			if (valid)
			{
				return;
			}

			// Get the table columns
			var tableColumns = GetType()
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
					errors.Add($"The definition of table '{GetType().FullName}' is missing column '{column}'.");
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

			// Mark as valid
			valid = true;
		}

		/// <summary>
		/// Map the columns and properties of this table
		/// </summary>
		private void Map()
		{
			// Check TablesMap cache
			if (TableMaps.Exists<TEntity>())
			{
				return;
			}

			// Get mapped properties and the corresponding column names
			var columns = from column in GetType().GetFields()
						  join property in typeof(TEntity).GetProperties() on column.Name equals property.Name
						  where property.GetCustomAttribute<IgnoreAttribute>() == null
						  select new MappedColumn
						  (
							  column: adapter.Escape(column.GetValue(this).ToString()),
						  	  property: property
						  );

			// Get ID property
			var id = GetPropertyWith<IdAttribute>("Id");

			// Create Table Map
			var map = new TableMap(ToString(), columns.ToList(), id);

			// Get Version property
			if (useVersion)
			{
				map.VersionColumn = GetPropertyWith<VersionAttribute>("Version");
			}

			// Add Map
			TableMaps.Add<TEntity>(map);

			// Get the property with the specified attribute
			MappedColumn GetPropertyWith<TAttribute>(string attr) where TAttribute : Attribute
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
		/// Escaped table name
		/// </summary>
		/// <returns>Escaped table name</returns>
		public override string ToString() => adapter.Escape(name);
	}
}
