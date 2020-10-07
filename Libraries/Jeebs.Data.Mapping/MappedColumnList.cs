using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Jx.Data.Mapping;

namespace Jeebs.Data.Mapping
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
