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
		public static string Escape(this IAdapter @this, IColumn col)
			=> @this.Escape(col.Name, col.Alias, col.Table);

		/// <summary>
		/// Escape a list of columns
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="columns">Columns</param>
		public static List<string> Escape(this IAdapter @this, IEnumerable<string> columns)
			=> (from c in columns select @this.Escape(c)).ToList();

		/// <summary>
		/// Escape a list of columns
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="columns">Columns</param>
		public static List<string> Escape(this IAdapter @this, IColumnList columns)
			=> (from c in columns select @this.Escape(c)).ToList();

		/// <summary>
		/// Escape a list of mapped columns
		/// </summary>
		/// <param name="this">IAdapter</param>
		/// <param name="columns">Mapped columns</param>
		public static List<string> Escape(this IAdapter @this, IMappedColumnList columns)
			=> (from c in columns select @this.Escape(c)).ToList();

		/// <summary>
		/// Extracts and escapes matching columns
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		public static string ExtractEscapeAndJoin<T>(this IAdapter @this, params Table[] tables)
		{
			// Extract matching columns from the specified tables
			var columns = Extract<T>.From(tables);

			// Escape each column
			var select = new List<string>();
			foreach (var c in columns)
			{
				select.Add(@this.Escape(c));
			}

			// Join using column separator
			return string.Join($"{@this.ColumnSeparator} ", select);
		}
	}
}
