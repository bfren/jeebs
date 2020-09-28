using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="IExtractedColumns"/>
	public sealed class ExtractedColumns : List<IExtractedColumn>, IExtractedColumns
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public ExtractedColumns() { }

		/// <summary>
		/// Construct object from IEnumerable
		/// </summary>
		/// <param name="ienum">IEnumerable</param>
		public ExtractedColumns(IEnumerable<IExtractedColumn> ienum) : base(ienum) { }
	}
}
