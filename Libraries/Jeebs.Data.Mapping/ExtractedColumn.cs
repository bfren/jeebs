using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="IExtractedColumn"/>
	public sealed class ExtractedColumn : IExtractedColumn
	{
		/// <inheritdoc/>
		public string Table { get; }

		/// <inheritdoc/>
		public string Column { get; }

		/// <inheritdoc/>
		public string Alias { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="table">Escaped Table Name</param>
		/// <param name="column">Escaped Column Name</param>
		/// <param name="alias">Column Alias</param>
		public ExtractedColumn(string table, string column, string alias)
			=> (Table, Column, Alias) = (table, column, alias);

		/// <summary>
		/// Extracted Column Comparer
		/// </summary>
		public class Comparer : IEqualityComparer<IExtractedColumn>
		{
			/// <summary>
			/// Returns true if the two aliases are identical
			/// </summary>
			/// <param name="x">IExtractedColumn 1</param>
			/// <param name="y">IExtractedColumn 2</param>
			/// <returns>True if the aliases of the two columns are identical</returns>
			public bool Equals(IExtractedColumn x, IExtractedColumn y)
				=> x.Alias == y.Alias;

			/// <summary>
			/// Return object's hash code
			/// </summary>
			/// <param name="obj">IExtractedColumn</param>
			/// <returns>Hash code</returns>
			public int GetHashCode(IExtractedColumn obj) 
				=> obj.Alias.GetHashCode();
		}
	}
}
