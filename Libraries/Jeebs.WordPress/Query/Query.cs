using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Enums;
using Jeebs.WordPress.Tables;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query wrapper
	/// </summary>
	public sealed class Query : Data.Query
	{
		/// <summary>
		/// IWpDb
		/// </summary>
		private readonly IWpDb db;

		/// <summary>
		/// Setup object
		/// </summary>
		/// <param name="db">IWpDb</param>
		public Query(IWpDb db) : base(db) => this.db = db;

		/// <summary>
		/// Query Posts
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="modifyOptions">[Optional] Action to modify the options for this query</param>
		public QueryExec<T> Posts<T>(Action<Posts.QueryOptions>? modifyOptions = null)
		{
			// Create and modify options
			var options = new Posts.QueryOptions();
			modifyOptions?.Invoke(options);

			// Build query
			var args = new Posts.QueryBuilder(db).Build<T>(options);

			// Return
			return new QueryExec<T>(UnitOfWork, args);
		}
	}
}
