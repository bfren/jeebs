using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Perform additional functions on ITable objects
	/// </summary>
	public static class TableExtensions
	{
		/// <summary>
		/// Extract and merge columns that match property names in <typeparamref name="TModel"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="tables">Array of tables</param>
		public static ExtractedColumns ExtractColumns<TModel>(this Table[] tables)
		{
			// Extract matching columns from each of the tables
			var mappedColumns = new List<ExtractedColumns>();
			foreach (var table in tables)
			{
				mappedColumns.Add(Extract<TModel>.From(table));
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
	}
}
