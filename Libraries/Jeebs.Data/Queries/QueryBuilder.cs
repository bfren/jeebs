using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Builds a Query in a fluent manner
	/// </summary>
	public sealed partial class QueryBuilder
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		private readonly IUnitOfWork unitOfWork;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		internal QueryBuilder(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

		/// <summary>
		/// Query Stage 2: Set the options for this query
		/// </summary>
		/// <typeparam name="TOptions">QueryOptions</typeparam>
		/// <param name="modify">[Optional] Action to modify default options</param>
		public QueryWithOptions<TOptions> WithOptions<TOptions>(Action<TOptions>? modify = null)
			where TOptions : QueryOptions, new()
		{
			// Create options
			var options = new TOptions();
			modify?.Invoke(options);

			// Pass to next stage
			return new QueryWithOptions<TOptions>(unitOfWork, options);
		}
	}
}
