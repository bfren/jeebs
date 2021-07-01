// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Linq;
using Jeebs.Data.Mapping;

namespace F.DataF
{
	public static partial class QueryF
	{
		/// <summary>
		/// Get columns from a table that match properties in <typeparamref name="TModel"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="table">Table</param>
		public static ColumnList GetColumnsFromTable<TModel>(ITable table)
		{
			// Get the list of properties
			var tableProperties = table.GetType().GetProperties();

			// Holds the list of column names being extracted
			var extracted = new List<IColumn>();
			foreach (var property in GetModelProperties<TModel>())
			{
				// Get the corresponding field
				var field = tableProperties.SingleOrDefault(p => p.Name == property.Name);

				// If the table field is not present in the model, continue
				if (field is null)
				{
					continue;
				}

				// Add the column to the extraction list
				if (field.GetValue(table) is string column)
				{
					var alias = property.Name;
					extracted.Add(new Column(table.GetName(), column, alias));
				}
			}

			// Return extracted columns
			return new ColumnList(extracted);
		}
	}
}
