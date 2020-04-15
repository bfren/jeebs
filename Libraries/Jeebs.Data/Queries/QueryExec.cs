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
		/// QueryParts
		/// </summary>
		private readonly QueryParts<TModel> parts;

		/// <summary>
		/// Setup object
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="parts">QueryParts</param>
		public QueryExec(IUnitOfWork unitOfWork, QueryParts<TModel> parts)
		{
			this.unitOfWork = unitOfWork;
			this.parts = parts;
		}

		/// <summary>
		/// Returns the number of items to be retrieved by the current query parts
		/// </summary>
		public async Task<Result<long>> Count()
		{
			// Store select
			var actualSelect = parts.Select;

			// Get count query
			parts.Select = unitOfWork.Adapter.GetSelectCount();
			var countQuery = unitOfWork.Adapter.Retrieve(parts);

			// Execute
			var count = await unitOfWork.ExecuteScalarAsync<long>(countQuery, parts.Parameters);

			// Reset select and return
			parts.Select = actualSelect;
			return count;
		}

		/// <summary>
		/// Retrieves the items using the current query parts
		/// </summary>
		public async Task<Result<IEnumerable<TModel>>> Retrieve()
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
		public async Task<Result<PagedList<TModel>>> RetrievePaged(long page)
		{
			// Get the count
			var count = await Count();
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
			var items = await Retrieve();
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
