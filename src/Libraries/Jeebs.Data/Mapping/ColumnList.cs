// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
