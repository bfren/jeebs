// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
