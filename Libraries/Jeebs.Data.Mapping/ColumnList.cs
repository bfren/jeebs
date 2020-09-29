using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="IColumnList"/>
	public sealed class ColumnList : List<IColumn>, IColumnList
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public ColumnList() { }

		/// <summary>
		/// Construct object from IEnumerable
		/// </summary>
		/// <param name="ienum">IEnumerable</param>
		public ColumnList(IEnumerable<IColumn> ienum) : base(ienum) { }
	}
}
