using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// IAdapter extensions - columns
	/// </summary>
	public static partial class AdapterExtensions
	{
		/// <summary>
		/// Escape an IColumn
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="col">IColumn</param>
		public static string EscapeColumn(this IAdapter @this, IColumn col)
			=> @this.EscapeColumn(col.Name, col.Alias, col.Table);

		/// <summary>
		/// Escape a list of columns
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="columns">Columns</param>
		public static List<string> EscapeColumns(this IAdapter @this, IEnumerable<string> columns)
			=> (from c in columns select @this.Escape(c)).ToList();

		/// <summary>
		/// Escape a list of columns
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="columns">Columns</param>
		public static List<string> EscapeColumns(this IAdapter @this, IColumnList columns)
			=> (from c in columns select EscapeColumn(@this, c)).ToList();

		/// <summary>
		/// Escape a list of mapped columns
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="columns">Mapped columns</param>
		public static List<string> EscapeColumns(this IAdapter @this, IMappedColumnList columns)
			=> (from c in columns select EscapeColumn(@this, c)).ToList();

		/// <summary>
		/// Join list of ExtractedColumn objects
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="columns">IColumns</param>
		public static string EscapeAndJoinColumns(this IAdapter @this, IColumnList columns)
		{
			// Get each column
			var select = new List<string>();
			foreach (var c in columns)
			{
				select.Add(EscapeColumn(@this, c));
			}

			// Return joined
			return string.Join($"{@this.ColumnSeparator} ", select);
		}

		/// <summary>
		/// Extracts and escapes matching columns
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		public static string ExtractEscapeAndJoinColumns<T>(this IAdapter @this, params Table[] tables)
			=> EscapeAndJoinColumns(@this, Extract<T>.From(tables));
	}
}
