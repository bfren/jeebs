// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static F.OptionF;
using Msg = Jeebs.Data.ExtractMsg;

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
		private static readonly ConcurrentDictionary<string, ColumnList> cache = new();

		/// <summary>
		/// Properties of <typeparamref name="TModel"/> that have not been marked with <see cref="IgnoreAttribute"/>
		/// </summary>
		private static readonly IEnumerable<PropertyInfo> modelProperties;

		/// <summary>
		/// Get model properties
		/// </summary>
		static Extract() =>
			modelProperties = from p in typeof(TModel).GetProperties()
							  where p.GetCustomAttribute<IgnoreAttribute>() == null
							  select p;

		/// <summary>
		/// Extract columns from specified tables
		/// </summary>
		/// <param name="tables">List of tables</param>
		public static Option<ColumnList> From(params ITable[] tables)
		{
			// If no tables, return empty extracted list
			if (tables.Length == 0)
			{
				return new ColumnList();
			}

			// Extract distinct columns
			return
				Return(
					() => from table in tables
						  from column in ExtractColumnsFromTable(table)
						  select column,
					e => new Msg.ErrorExtractingColumnsFromTableExceptionMsg(e)
				)
				.SwitchIf(
					x => x.Any(),
					_ => None<IEnumerable<IColumn>, Msg.NoColumnsExtractedFromTableMsg>()
				)
				.Map(
					x => x.Distinct(new Column.AliasComparer()),
					e => new Msg.ErrorGettingDistinctColumnsExceptionMsg(e)
				)
				.Map(
					x => new ColumnList(x),
					DefaultHandler
				);
		}

		/// <summary>
		/// Extract columns from a single table
		/// </summary>
		/// <param name="table">Table</param>
		private static ColumnList ExtractColumnsFromTable(ITable table) =>
			cache.GetOrAdd(table.ToString() ?? throw new ArgumentNullException(nameof(table)), tableName =>
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

	/// <summary>Messages</summary>
	public static class ExtractMsg
	{
		/// <summary>An error occurred extracting columns from a table</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record ErrorExtractingColumnsFromTableExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>An error occurred getting distinct columns</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record ErrorGettingDistinctColumnsExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>No matching columns were extracted from the table</summary>
		public sealed record NoColumnsExtractedFromTableMsg : IMsg { }
	}
}
