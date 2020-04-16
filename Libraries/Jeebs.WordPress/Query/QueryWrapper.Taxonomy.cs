using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query wrapper
	/// </summary>
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Taxonomy
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="modifyOptions">[Optional] Action to modify the options for this query</param>
		public Query<T> QueryTaxonomy<T>(Action<QueryTaxonomy.Options>? modifyOptions = null)
		{
			return StartNewQuery()
				.WithOptions(modifyOptions)
				.WithBuilder(new QueryTaxonomy.Builder<T>(db));
		}
	}
}
