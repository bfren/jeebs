using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
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
		/// <param name="this">IAdapter</param>
		/// <param name="table">Table</param>
		/// <param name="column">Column</param>
		public static string Escape<T>(this IAdapter @this, T table, Func<T, string> column)
			where T : Table
			=> @this.EscapeAndJoin(table, column(table));

		/// <summary>
		/// Extracts and escapes matching columns
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		public static string Extract<T>(this IAdapter @this, params Table[] tables)
			=> @this.Join(Mapping.Extract<T>.From(tables));

		/// <summary>
		/// Join list of ExtractedColumn objects
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="columns">IColumns</param>
		public static string Join(this IAdapter @this, IColumnList columns)
		{
			// Get each column
			var select = new List<string>();
			foreach (var c in columns)
			{
				select.Add(GetColumn(@this, c));
			}

			// Return joined
			return string.Join($"{@this.ColumnSeparator} ", select);
		}

		/// <summary>
		/// Escape a Column
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="col">IColumn</param>
		public static string GetColumn(this IAdapter @this, IColumn col)
			=> @this.EscapeColumn(name: col.Name, alias: col.Alias, table: col.Table);
	}
}
