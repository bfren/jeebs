using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Holds information about a column extracted from a Model for the select clause of a query
	/// </summary>
	public sealed class ExtractedColumn : IExtractedColumn
	{
		/// <summary>
		/// Escaped Table Name
		/// </summary>
		public string Table { get; }

		/// <summary>
		/// Escaped Column Name
		/// </summary>
		public string Column { get; }

		/// <summary>
		/// Column Alias
		/// </summary>
		public string Alias { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="table">Escaped Table Name</param>
		/// <param name="column">Escaped Column Name</param>
		/// <param name="alias">Column Alias</param>
		public ExtractedColumn(string table, string column, string alias)
		{
			Table = table;
			Column = column;
			Alias = alias;
		}

		/// <summary>
		/// Extracted Column Comparer
		/// </summary>
		public class Comparer : IEqualityComparer<IExtractedColumn>
		{
			/// <summary>
			/// Returns true if the two columns are identical
			/// </summary>
			/// <param name="x">IExtractedColumn 1</param>
			/// <param name="y">IExtractedColumn 2</param>
			/// <returns>True if the two columns are identical</returns>
			public bool Equals(IExtractedColumn x, IExtractedColumn y)
				=> x.Table.Equals(y.Table) && x.Column.Equals(y.Column) && x.Alias.Equals(y.Alias);

			/// <summary>
			/// Return object's hash code
			/// </summary>
			/// <param name="obj">IExtractedColumn</param>
			/// <returns>Hash code</returns>
			public int GetHashCode(IExtractedColumn obj)
				=> obj.GetHashCode();
		}
	}
}
