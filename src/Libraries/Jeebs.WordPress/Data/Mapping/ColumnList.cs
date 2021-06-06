// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.WordPress.Data.Mapping
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
		/// <param name="collection">IEnumerable</param>
		public ColumnList(IEnumerable<IColumn> collection) : base(collection) { }
	}
}
