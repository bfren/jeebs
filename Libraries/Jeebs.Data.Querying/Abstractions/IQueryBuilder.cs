using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Builds a Query in a fluent manner
	/// </summary>
	public interface IQueryBuilder
	{
		/// <summary>
		/// Query Stage 2: Set the options for this query
		/// </summary>
		/// <typeparam name="TOptions">QueryOptions</typeparam>
		/// <param name="modify">[Optional] Action to modify default options</param>
		public IQueryWithOptions<TOptions> WithOptions<TOptions>(Action<TOptions>? modify = null)
			where TOptions : QueryOptions, new();
	}
}
