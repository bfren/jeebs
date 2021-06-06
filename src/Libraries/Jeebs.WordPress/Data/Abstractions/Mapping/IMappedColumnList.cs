// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.WordPress.Data.Mapping
{
	/// <summary>
	/// Holds and processes a list of <seealso cref="IMappedColumn"/> objects
	/// </summary>
	public interface IMappedColumnList : IList<IMappedColumn> { }
}
