// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using Jeebs.Data.Map;

namespace Jeebs.Data;

public static partial class DataF
{
	/// <summary>
	/// Get columns from a table that match properties in <typeparamref name="TModel"/>.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="table">Table.</param>
	public static ColumnList GetColumnsFromTable<TModel>(ITable table)
	{
		// Get the list of properties
		var tableProperties = table.GetType().GetProperties();

		// Holds the list of column names being extracted
		var extracted = new List<IColumn>();
		foreach (var modelProperty in GetModelProperties<TModel>())
		{
			// Get the corresponding table property
			var tableProperty = tableProperties.SingleOrDefault(p => p.Name == modelProperty.Name);

			// If the model property is not present in the table, continue
			if (tableProperty is null)
			{
				continue;
			}

			// Add the column to the extraction list
			if (tableProperty.GetValue(table) is string column)
			{
				extracted.Add(new Column(table, column, tableProperty));
			}
		}

		// Return extracted columns
		return new ColumnList(extracted);
	}
}
