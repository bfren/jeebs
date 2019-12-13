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
	public abstract class Table<TEntity> : ITable
		where TEntity : IEntity
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
		private bool valid;

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
		/// Table name
		/// </summary>
		/// <returns>Table name</returns>
		public override string ToString() => name;

		#region Mapping

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
			var map = new TableMap(adapter.Escape(name), columns.ToList(), id);

			// Get Version property
			if (useVersion)
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

		#endregion

		#region Extracting

		/// <summary>
		/// Cached maps of TModel classes to columns
		/// </summary>
		private static readonly Dictionary<Type, ExtractedColumns> cache = new Dictionary<Type, ExtractedColumns>();

		/// <summary>
		/// Extract the column names for the table entity
		/// </summary>
		public ExtractedColumns Extract() => Extract(typeof(TEntity));


		/// <summary>
		/// Extract the column names for the model type
		/// </summary>
		/// <typeparam name="TModel">Model Type</typeparam>
		public ExtractedColumns Extract<TModel>() => Extract(typeof(TModel));

		/// <summary>
		/// Extract the column names for <paramref name="modelType"/>
		/// </summary>
		/// <param name="modelType">Model Type</param>
		private ExtractedColumns Extract(Type modelType)
		{
			// Check the cache first to see if this model has already been extracted
			if (cache.TryGetValue(modelType, out ExtractedColumns value))
			{
				return value;
			}

			// Get the list of properties from the model
			var modelProperties = modelType.GetProperties().Where(p => p.GetCustomAttribute<IgnoreAttribute>() == null);

			// Get the list of mapped properties
			var mappedProperties = TableMaps.GetColumns<TEntity>();

			// Holds the list of column names being extracted
			var extracted = new ExtractedColumns();

			foreach (var prop in modelProperties)
			{
				// Get the corresponding mapped property
				var mapped = mappedProperties.SingleOrDefault(p => p.Property.Name == prop.Name);

				// If the column has not been mapped, continue
				if (mapped == null)
				{
					continue;
				}

				// Add the column to the extraction list
				extracted.Add(new ExtractedColumn(adapter.Escape(name), mapped.Column, mapped.Property.Name));
			}

			// Return list of extracted columns
			return extracted;
		}

		#endregion
	}
}
