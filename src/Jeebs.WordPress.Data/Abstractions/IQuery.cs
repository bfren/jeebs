// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data.Querying;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Generic WordPress Query interface
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">Entity ID type</typeparam>
	/// <typeparam name="TOptions">Query Options type</typeparam>
	public interface IQuery<TEntity, TId, TOptions>
		where TEntity : IWithId<TId>
		where TId : StrongId
		where TOptions : IQueryOptions<TEntity, TId>
	{
		/// <summary>
		/// Return / modify query options
		/// </summary>
		/// <param name="opt">Default options</param>
		public delegate TOptions GetOptions(TOptions opt);

		/// <summary>
		/// Run a query and return multiple items
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="opt">Function to return query options</param>
		Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(GetOptions opt)
			where TModel : IWithId;
	}
}
