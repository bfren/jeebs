using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Util;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IQuery{T}"/>
	public sealed class Query<T> : IQuery<T>
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		private readonly IUnitOfWork unitOfWork;

		/// <summary>
		/// QueryParts
		/// </summary>
		private readonly IQueryParts parts;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="parts">IQueryParts</param>
		internal Query(IUnitOfWork unitOfWork, IQueryParts parts)
		{
			this.unitOfWork = unitOfWork;
			this.parts = parts;
		}

		/// <inheritdoc/>
		public async Task<IResult<long>> GetCountAsync()
		{
			// Store original SELECT
			var originalSelect = parts.Select;

			// Alter SELECT
			parts.Select = unitOfWork.Adapter.GetSelectCount();

			// Get count query
			var countQuery = unitOfWork.Adapter.Retrieve(parts);

			// Execute
			var count = await unitOfWork.ExecuteScalarAsync<long>(countQuery, parts.Parameters);

			// Restore SELECT and return
			parts.Select = originalSelect;
			return count;
		}

		/// <inheritdoc/>
		public async Task<IResult<IEnumerable<T>>> ExecuteQueryAsync()
		{
			// Get query
			var query = unitOfWork.Adapter.Retrieve(parts);

			// Execute and return
			return await unitOfWork.QueryAsync<T>(query, parts.Parameters);
		}

		/// <inheritdoc/>
		public async Task<IResult<IPagedList<T>>> ExecuteQueryAsync(long page)
		{
			// Get the count
			var count = await GetCountAsync();
			if (count.Err is IErrorList)
			{
				return Result.Failure<IPagedList<T>>(count.Err);
			}

			// Get paging values
			var values = new PagingValues(count.Val, page, parts.Limit ?? Defaults.PagingValues.ItemsPer);

			// Set the OFFSET and LIMIT values
			parts.Offset = (values.Page - 1) * values.ItemsPer;
			parts.Limit = values.ItemsPer;

			// Get the items
			var items = await ExecuteQueryAsync();
			if (items.Err is IErrorList)
			{
				return Result.Failure<IPagedList<T>>(items.Err);
			}

			// Add the items to the list and return success
			var list = new PagedList<T>(items.Val);
			return Result.Success<IPagedList<T>>(list);
		}
	}
}
