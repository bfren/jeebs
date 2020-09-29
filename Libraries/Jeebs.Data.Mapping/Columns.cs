using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="IColumns"/>
	public sealed class Columns : List<IColumn>, IColumns
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public Columns() { }

		/// <summary>
		/// Construct object from IEnumerable
		/// </summary>
		/// <param name="ienum">IEnumerable</param>
		public Columns(IEnumerable<IColumn> ienum) : base(ienum) { }
	}
}
