using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Extract columns from a table that match <see cref="TModel"/>
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	public static class Extract<TModel>
	{
		/// <summary>
		/// Cached maps of table classes to columns
		/// </summary>
		private static readonly Dictionary<string, ExtractedColumns> cache = new Dictionary<string, ExtractedColumns>();

		/// <summary>
		/// Model properties
		/// </summary>
		private static readonly IEnumerable<PropertyInfo> properties;

		/// <summary>
		/// Get properties for <see cref="TModel"/> that have not been marked with <see cref="IgnoreAttribute"/>
		/// </summary>
		static Extract() => properties = typeof(TModel).GetProperties().Where(p => p.GetCustomAttribute<IgnoreAttribute>() == null);

		/// <summary>
		/// Extract columns from specified table
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="table">Table</param>
		public static ExtractedColumns From<TTable>(TTable table)
			where TTable : Table
		{
			// Check the cache first to see if this model has already been used against this table
			if (cache.TryGetValue(table.ToString(), out ExtractedColumns value))
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
			cache.Add(table.ToString(), extracted);

			// Return extracted columns
			return extracted;
		}
	}
}
