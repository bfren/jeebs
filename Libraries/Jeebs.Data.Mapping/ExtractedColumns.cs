using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Alias for a list of ExtractedColumn objects
	/// </summary>
	public sealed class ExtractedColumns : List<IExtractedColumn>, IExtractedColumns
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public ExtractedColumns() { }

		/// <summary>
		/// Construct object from Enumerable
		/// </summary>
		/// <param name="ienum">IEnumerable</param>
		public ExtractedColumns(IEnumerable<IExtractedColumn> ienum) : base(ienum) { }
	}
}
