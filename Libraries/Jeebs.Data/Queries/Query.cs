using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Util;

namespace Jeebs.Data
{
	/// <summary>
	/// Query
	/// </summary>
	/// <typeparam name="TModel">Return Model type</typeparam>
	public sealed class Query<TModel>
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		private readonly IUnitOfWork unitOfWork;

		/// <summary>
		/// QueryParts
		/// </summary>
		private readonly QueryParts<TModel> parts;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="parts">QueryParts</param>
		internal Query(IUnitOfWork unitOfWork, QueryParts<TModel> parts)
		{
			this.unitOfWork = unitOfWork;
			this.parts = parts;
		}

		/// <summary>
		/// Returns the number of items to be retrieved by the current query parts
		/// </summary>
		public async Task<Result<long>> GetCount()
		{
			// Use QueryParts but alter SELECT
			var countParts = parts.Clone();
			countParts.Select = unitOfWork.Adapter.GetSelectCount();

			// Get count query
			var countQuery = unitOfWork.Adapter.Retrieve(countParts);

			// Execute
			return await unitOfWork.ExecuteScalarAsync<long>(countQuery, countParts.Parameters);
		}

		/// <summary>
		/// Retrieves the items using the current query parts
		/// </summary>
		public async Task<Result<IEnumerable<TModel>>> ExecuteQuery()
		{
			// Get query
			var query = unitOfWork.Adapter.Retrieve(parts);

			// Execute and return
			return await unitOfWork.QueryAsync<TModel>(query, parts.Parameters);
		}

		/// <summary>
		/// Retrieves the items using the current query parts, using LIMIT / OFFSET to select only the items on a particular page
		/// </summary>
		/// <param name="page">Current page number</param>
		public async Task<Result<PagedList<TModel>>> ExecuteQuery(long page)
		{
			// Get the count
			var count = await GetCount();
			if (count.Err is ErrorList)
			{
				return Result.Failure<PagedList<TModel>>(count.Err);
			}

			// Get paging values
			var values = new PagingValues(count.Val, page, parts.Limit ?? PagingValues.Defaults.ItemsPer);

			// Set the OFFSET and LIMIT values
			parts.Offset = (values.Page - 1) * values.ItemsPer;
			parts.Limit = values.ItemsPer;

			// Get the items
			var items = await ExecuteQuery();
			if (items.Err is ErrorList)
			{
				return Result.Failure<PagedList<TModel>>(items.Err);
			}

			// Add the items to the list and return success
			var list = new PagedList<TModel>(items.Val);
			return Result.Success(list);
		}
	}
}
