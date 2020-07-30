using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// IAdapter extensions - columns
	/// </summary>
	public static partial class AdapterExtensions
	{
		/// <summary>
		/// Escape and join a column on a table
		/// </summary>
		/// <typeparam name="T">Table type</typeparam>
		/// <param name="adapter">IAdapter</param>
		/// <param name="table">Table</param>
		/// <param name="column">Column</param>
		public static string Escape<T>(this IAdapter adapter, T table, Func<T, string> column)
			where T : Table
			=> adapter.EscapeAndJoin(table, column(table));

		/// <summary>
		/// Extracts and escapes matching columns
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="adapter">IAdapter</param>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		public static string Extract<T>(this IAdapter adapter, params Table[] tables)
			=> adapter.Join(Data.Extract<T>.From(tables));

		/// <summary>
		/// Join list of ExtractedColumn objects
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		/// <param name="columns">IExtractedColumns</param>
		public static string Join(this IAdapter adapter, IExtractedColumns columns)
		{
			// Get each column
			var select = new List<string>();
			foreach (var c in columns)
			{
				select.Add(GetColumn(adapter, c));
			}

			// Return joined
			return string.Join(adapter.ColumnSeparator, select);
		}

		/// <summary>
		/// Get an ExtractedColumn
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		/// <param name="col">IExtractedColumn</param>
		public static string GetColumn(this IAdapter adapter, IExtractedColumn col)
			=> adapter.EscapeColumn(string.Concat(col.Table, adapter.SchemaSeparator, col.Column), col.Alias);

		/// <summary>
		/// Get a MappedColumn
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		/// <param name="col">IMappedColumn</param>
		public static string GetColumn(this IAdapter adapter, IMappedColumn col)
			=> adapter.EscapeColumn(col.Column, col.Property.Name);
	}
}
