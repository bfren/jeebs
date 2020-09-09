using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jeebs.Data
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
		private static readonly ConcurrentDictionary<string, IExtractedColumns> cache = new ConcurrentDictionary<string, IExtractedColumns>();

		/// <summary>
		/// Model properties
		/// </summary>
		private static readonly IEnumerable<PropertyInfo> properties;

		/// <summary>
		/// Get properties for <typeparamref name="TModel"/> that have not been marked with <see cref="IgnoreAttribute"/>
		/// </summary>
		static Extract()
			=> properties = typeof(TModel).GetProperties().Where(p => p.GetCustomAttribute<IgnoreAttribute>() == null);

		/// <summary>
		/// Extract columns from specified tables
		/// </summary>
		/// <param name="tables">List of tables</param>
		public static IExtractedColumns From(params Table[] tables)
		{
			// Extract matching columns from each of the tables
			var mappedColumns = new List<IExtractedColumns>();
			foreach (var table in tables)
			{
				mappedColumns.Add(ExtractSingle(table));
			}

			// Now create a master list of all the extracted columns
			var mergedColumns = new ExtractedColumns();
			foreach (var mapped in mappedColumns)
			{
				mergedColumns.AddRange(mapped);
			}

			// Make sure some columns were found
			if (!mergedColumns.Any())
			{
				throw new Jx.Data.MappingException("No columns were extracted.");
			}

			// Get only distinct columns
			var distinctColumns = mergedColumns.Distinct(new ExtractedColumn.Comparer());

			// Return
			return new ExtractedColumns(distinctColumns);
		}

		/// <summary>
		/// Extract columns from a single table
		/// </summary>
		/// <param name="table">Table</param>
		private static IExtractedColumns ExtractSingle(Table table)
		{
			// Check the cache first to see if this model has already been used against this table
			if (cache.TryGetValue(table.ToString(), out IExtractedColumns value))
			{
				return value;
			}

			// Get the list of mapped properties
			var tableFields = table.GetType().GetFields();

			// Holds the list of column names being extracted
			var extracted = new ExtractedColumns();

			foreach (var prop in properties)
			{
				// Get the corresponding column
				var column = tableFields.SingleOrDefault(p => p.Name == prop.Name);

				// If the column has not been mapped, continue
				if (column is null)
				{
					continue;
				}

				// Add the column to the extraction list
				extracted.Add(new ExtractedColumn(table.ToString(), column.GetValue(table).ToString(), prop.Name));
			}

			// Add to the cache
			cache.TryAdd(table.ToString(), extracted);

			// Return extracted columns
			return extracted;
		}
	}
}
