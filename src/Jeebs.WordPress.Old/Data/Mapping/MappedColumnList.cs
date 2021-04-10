// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs.WordPress.Data.Mapping
{
	/// <inheritdoc cref="IMappedColumnList"/>
	public sealed class MappedColumnList : List<IMappedColumn>, IMappedColumnList
	{
		/// <summary>
		/// Create object
		/// </summary>
		public MappedColumnList() { }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="collection">IEnumerable</param>
		public MappedColumnList(IEnumerable<IMappedColumn> collection) : base(collection) { }
	}
}
