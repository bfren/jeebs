using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Holds and processes a list of <seealso cref="IMappedColumn"/> objects
	/// </summary>
	public interface IMappedColumnList : IList<IMappedColumn> { }
}
