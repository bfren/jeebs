using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="IColumn"/>
	public class Column : IColumn
	{
		/// <inheritdoc/>
		public string Table { get; }

		/// <inheritdoc/>
		public string Name { get; }

		/// <inheritdoc/>
		public string Alias { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="table">Table Name</param>
		/// <param name="name">Column Name</param>
		/// <param name="alias">Column Alias</param>
		public Column(string table, string name, string alias)
			=> (Table, Name, Alias) = (table, name, alias);

		/// <summary>
		/// Extracted Column Comparer
		/// </summary>
		public class Comparer : IEqualityComparer<IColumn>
		{
			/// <summary>
			/// Returns true if the two aliases are identical
			/// </summary>
			/// <param name="x">IColumn 1</param>
			/// <param name="y">IColumn 2</param>
			/// <returns>True if the aliases of the two columns are identical</returns>
			public bool Equals(IColumn x, IColumn y)
				=> x.Alias == y.Alias;

			/// <summary>
			/// Return object's hash code
			/// </summary>
			/// <param name="obj">IColumn</param>
			/// <returns>Hash code</returns>
			public int GetHashCode(IColumn obj) 
				=> obj.Alias.GetHashCode();
		}
	}
}
