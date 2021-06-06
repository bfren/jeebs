// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.Data
{
	/// <summary>
	/// List of <see cref="IMappedColumn"/> objects
	/// </summary>
	public sealed class MappedColumnList : List<IMappedColumn>, IMappedColumnList
	{
		/// <summary>
		/// Create empty list
		/// </summary>
		public MappedColumnList() { }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="collection">IEnumerable</param>
		public MappedColumnList(IEnumerable<IMappedColumn> collection) : base(collection) { }
	}
}
