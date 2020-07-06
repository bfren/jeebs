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
		public async Task<IR<long>> GetCountAsync(IOk<dynamic> r)
		{
			// Store original SELECT
			var originalSelect = parts.Select;

			// Alter SELECT
			parts.Select = unitOfWork.Adapter.GetSelectCount();

			// Get count query
			var countQuery = unitOfWork.Adapter.Retrieve(parts);

			// Execute
			var count = await unitOfWork.ExecuteScalarAsync<long>(r, countQuery, parts.Parameters);

			// Restore SELECT and return
			parts.Select = originalSelect;
			return count;
		}

		/// <inheritdoc/>
		public async Task<IR<IEnumerable<T>>> ExecuteQueryAsync(IOk<dynamic> r)
		{
			// Get query
			var query = unitOfWork.Adapter.Retrieve(parts);

			// Execute and return
			return await unitOfWork.QueryAsync<T>(r, query, parts.Parameters);
		}

		/// <inheritdoc/>
		public async Task<IR<PagedList<T>>> ExecuteQueryAsync(IOk<dynamic> r, long page)
		{
			// Get the count
			async Task<IR<long>> getCount(IOk<dynamic> r) => await GetCountAsync(r);

			// Get paging values
			async Task getPagingValues(IOkV<long> r)
			{
				// Create values object
				var values = new PagingValues(r.Val, page, parts.Limit ?? Defaults.PagingValues.ItemsPer);

				// Set the OFFSET and LIMIT values
				parts.Offset = (values.Page - 1) * values.ItemsPer;
				parts.Limit = values.ItemsPer;
			}

			// Get the items
			async Task<IR<IEnumerable<T>>> getItems(IOkV<long> r)
			{
				return await ExecuteQueryAsync(r.OkNew<dynamic>()).ConfigureAwait(false);
			}

			// Convert to a paged list
			async Task<IR<PagedList<T>>> getPagedList(IOkV<IEnumerable<T>> r)
			{
				var list = new PagedList<T>(r.Val);
				return r.OkV(list);
			}

			// Run chain
			return await r
				.LinkMapAsync(getCount)
				.LinkAsync(getPagingValues)
				.LinkMapAsync(getItems)
				.LinkMapAsync(getPagedList);
		}
	}
}
