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
		/// Query Posts
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="modifyOptions">[Optional] Action to modify the options for this query</param>
		public QueryExec<T> QueryTaxonomy<T>(Action<Taxonomy.QueryOptions>? modifyOptions = null)
		{
			// Create and modify options
			var options = new Taxonomy.QueryOptions();
			modifyOptions?.Invoke(options);

			// Get Exec
			return new Taxonomy.QueryBuilder<T>(db)
				.Build(options)
				.GetExec(UnitOfWork);
		}
	}
}
