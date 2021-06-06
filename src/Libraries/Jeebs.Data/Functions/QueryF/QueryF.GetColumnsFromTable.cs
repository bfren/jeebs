// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Concurrent;
using System.Linq;
using Jeebs.Data;

namespace F.DataF
{
	public static partial class QueryF
	{
		private const bool enableColumnsCache = true;

		/// <summary>
		/// Cached maps of table classes to columns
		/// </summary>
		private static readonly ConcurrentDictionary<(Type table, Type model), ColumnList> columnsCache = new();

		/// <summary>
		/// Get columns from a table that match properties in <typeparamref name="TModel"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="table">Table</param>
		public static ColumnList GetColumnsFromTable<TModel>(ITable table)
		{
			return enableColumnsCache switch
			{
				true =>
					columnsCache.GetOrAdd((table.GetType(), typeof(TModel)), _ => get()),

				false =>
					get()
			};

			ColumnList get()
			{
				// Get the list of properties
				var tableProperties = table.GetType().GetProperties();

				// Holds the list of column names being extracted
				var extracted = new ColumnList();
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
				return extracted;
			}
		}
	}
}
