using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Holds information about a column extracted from a Model for the select clause of a query
	/// </summary>
	public sealed class ExtractedColumn
	{
		/// <summary>
		/// Escaped Table Name
		/// </summary>
		public string Table { get; set; }

		/// <summary>
		/// Escaped Column Name
		/// </summary>
		public string Column { get; set; }

		/// <summary>
		/// Escaped Column Alias
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="table">Escaped Table Name</param>
		/// <param name="column">Escaped Column Name</param>
		/// <param name="alias">Escaped Column Alias</param>
		public ExtractedColumn(string table, string column, string alias)
		{
			Table = table;
			Column = column;
			Alias = alias;
		}

		/// <summary>
		/// Extracted Column Comparer
		/// </summary>
		public class Comparer : IEqualityComparer<ExtractedColumn>
		{
			/// <summary>
			/// Returns true if the two columns are identical
			/// </summary>
			/// <param name="x">ExtractedColumn 1</param>
			/// <param name="y">ExtractedColumn 2</param>
			/// <returns>True if the two columns are identical</returns>
			public bool Equals(ExtractedColumn x, ExtractedColumn y)
			{
				return x.Table.Equals(y.Table) && x.Column.Equals(y.Column) && x.Alias.Equals(y.Alias);
			}

			/// <summary>
			/// Return object's hash code
			/// </summary>
			/// <param name="obj">ExtractedColumn</param>
			/// <returns>Hash code</returns>
			public int GetHashCode(ExtractedColumn obj) => obj.GetHashCode();
		}
	}
}
