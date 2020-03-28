using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query wrapper
	/// </summary>
	public sealed partial class Query : Data.Query
	{
		/// <summary>
		/// Query Taxonomy
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="modifyOptions">[Optional] Action to modify the options for this query</param>
		public QueryExec<T> QueryTaxonomy<T>(Action<QueryTaxonomy.Options>? modifyOptions = null)
		{
			// Create and modify options
			var options = new QueryTaxonomy.Options();
			modifyOptions?.Invoke(options);

			// Get Exec
			return new QueryTaxonomy.Builder<T>(db)
				.Build(options)
				.GetExec(UnitOfWork);
		}
	}
}
