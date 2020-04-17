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
		/// Shorthand for Table[].ExtractColumns and then IAdapter.Join
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		public static string Extract<T>(this IAdapter adapter, params Table[] tables)
		{
			return adapter.Join(tables.ExtractColumns<T>());
		}

		/// <summary>
		/// Joa list of ExtractedColumn objects
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

			// Return joined with a comma
			return string.Join(", ", select);
		}

		/// <summary>
		/// Get an ExtractedColumn
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		/// <param name="col">IExtractedColumn</param>
		public static string GetColumn(this IAdapter adapter, IExtractedColumn col)
		{
			return adapter.EscapeColumn(string.Concat(col.Table, adapter.Separator, col.Column), col.Alias);
		}

		/// <summary>
		/// Get a MappedColumn
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		/// <param name="col">IMappedColumn</param>
		public static string GetColumn(this IAdapter adapter, IMappedColumn col)
		{
			return adapter.EscapeColumn(col.Column, col.Property.Name);
		}
	}
}
