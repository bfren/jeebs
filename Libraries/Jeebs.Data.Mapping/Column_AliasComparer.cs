// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Collections.Generic;

namespace Jeebs.Data.Mapping
{
	public partial class Column
	{
		/// <summary>
		/// Column Alias Comparer
		/// </summary>
		public class AliasComparer : IEqualityComparer<IColumn>
		{
			/// <summary>
			/// Returns true if the two aliases are identical
			/// </summary>
			/// <param name="x">IColumn 1</param>
			/// <param name="y">IColumn 2</param>
			/// <returns>True if the aliases of the two columns are identical</returns>
			public bool Equals(IColumn? x, IColumn? y) =>
				x?.Alias == y?.Alias;

			/// <summary>
			/// Return object's hash code
			/// </summary>
			/// <param name="obj">IColumn</param>
			/// <returns>Hash code</returns>
			public int GetHashCode(IColumn obj) =>
				obj.Alias.GetHashCode();
		}
	}
}
