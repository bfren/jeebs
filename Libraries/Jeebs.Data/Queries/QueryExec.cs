using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Wrapper for executing a query, or the total number of items to be returned by that query
	/// </summary>
	/// <typeparam name="T">Entity type to be returned</typeparam>
	public sealed class QueryExec<T>
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		private readonly IUnitOfWork unitOfWork;

		/// <summary>
		/// QueryArgs
		/// </summary>
		private readonly QueryArgs args;

		/// <summary>
		/// Setup object
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="args">QueryArgs</param>
		public QueryExec(IUnitOfWork unitOfWork, QueryArgs args)
		{
			this.unitOfWork = unitOfWork;
			this.args = args;
		}

		/// <summary>
		/// Returns the number of items to be retrieved by the current query
		/// </summary>
		public async Task<Result<int>> Count()
		{
			// Store select
			var actualSelect = args.Select;

			// Get count query
			args.Select = unitOfWork.Adapter.GetSelectCount();
			var countQuery = unitOfWork.Adapter.Retrieve(args);

			// Execute
			var count = await unitOfWork.ExecuteScalarAsync<int>(countQuery, args.Parameters);

			// Reset select and return
			args.Select = actualSelect;
			return count;
		}

		/// <summary>
		/// Retrieves the items using the current query
		/// </summary>
		public async Task<Result<IEnumerable<T>>> Retrieve()
		{
			// Get query
			var query = unitOfWork.Adapter.Retrieve(args);

			// Execute and return
			return await unitOfWork.QueryAsync<T>(query, args.Parameters);
		}
	}
}
