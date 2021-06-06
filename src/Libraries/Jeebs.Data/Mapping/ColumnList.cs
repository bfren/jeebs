// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.Data
{
	/// <summary>
	/// List of <see cref="IColumn"/> objects
	/// </summary>
	public sealed class ColumnList : List<IColumn>
	{
		/// <summary>
		/// Create empty list
		/// </summary>
		public ColumnList() { }

		/// <summary>
		/// Construct object from IEnumerable
		/// </summary>
		/// <param name="collection">IEnumerable</param>
		public ColumnList(IEnumerable<IColumn> collection) : base(collection) { }
	}
}
