// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jeebs.WordPress.Data.Mapping
{
	/// <summary>
	/// Extract columns from a table that match <typeparamref name="TModel"/>
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	public static class Extract<TModel>
	{
		/// <summary>
		/// Cached maps of table classes to columns
		/// </summary>
		private static readonly ConcurrentDictionary<string, IColumnList> cache = new();

		/// <summary>
		/// Properties of <typeparamref name="TModel"/> that have not been marked with <see cref="IgnoreAttribute"/>
		/// </summary>
		private static readonly IEnumerable<PropertyInfo> modelProperties;

		/// <summary>
		/// Get model properties
		/// </summary>
		static Extract() =>
			modelProperties = typeof(TModel).GetProperties().Where(p => p.GetCustomAttribute<IgnoreAttribute>() == null);

		/// <summary>
		/// Extract columns from specified tables
		/// </summary>
		/// <param name="tables">List of tables</param>
		public static IColumnList From(params Table[] tables)
		{
			// If no tables, return empty extracted list
			if (tables.Length == 0)
			{
				return new ColumnList();
			}

			// Extract columns from each table
			var columns = from table in tables
						  from column in ExtractColumnsFromTable(table)
						  select column;

			// Make sure some columns were found
			if (!columns.Any())
			{
				throw new Jx.Data.Mapping.NoColumnsExtractedException(typeof(TModel), tables);
			}

			// Get only distinct columns
			var distinctColumns = columns.Distinct(new Column.AliasComparer());

			// Return
			return new ColumnList(distinctColumns);
		}

		/// <summary>
		/// Extract columns from a single table
		/// </summary>
		/// <param name="table">Table</param>
		private static IColumnList ExtractColumnsFromTable(Table table) =>
			cache.GetOrAdd(table.ToString(), tableName =>
			{
				// Get the list of properties
				var tableProperties = table.GetType().GetProperties();

				// Holds the list of column names being extracted
				var extracted = new ColumnList();
				foreach (var property in modelProperties)
				{
					// Get the corresponding field
					var field = tableProperties.SingleOrDefault(p => p.Name == property.Name);

					// If the table field is not present in the model, continue
					if (field is null)
					{
						continue;
					}

					// Add the column to the extraction list
					if (field.GetValue(table)?.ToString() is string column)
					{
						var alias = property.Name;
						extracted.Add(new Column(tableName, column, alias));
					}
				}

				// Return extracted columns
				return extracted;
			});
	}
}
