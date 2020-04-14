using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Wrapper for executing a query, or the total number of items to be returned by that query
	/// </summary>
	/// <typeparam name="TModel">Model type to be returned</typeparam>
	public sealed class QueryExec<TModel>
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		private readonly IUnitOfWork unitOfWork;

		/// <summary>
		/// QueryArgs
		/// </summary>
		private readonly QueryArgs<TModel> args;

		/// <summary>
		/// Setup object
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="args">QueryArgs</param>
		public QueryExec(IUnitOfWork unitOfWork, QueryArgs<TModel> args)
		{
			this.unitOfWork = unitOfWork;
			this.args = args;
		}

		/// <summary>
		/// Returns the number of items to be retrieved by the current query
		/// </summary>
		public async Task<Result<long>> Count()
		{
			// Store select
			var actualSelect = args.Select;

			// Get count query
			args.Select = unitOfWork.Adapter.GetSelectCount();
			var countQuery = unitOfWork.Adapter.Retrieve(args);

			// Execute
			var count = await unitOfWork.ExecuteScalarAsync<long>(countQuery, args.Parameters);

			// Reset select and return
			args.Select = actualSelect;
			return count;
		}

		/// <summary>
		/// Retrieves the items using the current query
		/// </summary>
		public async Task<Result<IEnumerable<TModel>>> Retrieve()
		{
			// Get query
			var query = unitOfWork.Adapter.Retrieve(args);

			// Execute and return
			return await unitOfWork.QueryAsync<TModel>(query, args.Parameters);
		}
	}
}
