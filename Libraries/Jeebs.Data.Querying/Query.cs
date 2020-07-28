using System;
using System.Collections.Generic;
using System.Linq;
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
		public async Task<IR<long>> GetCountAsync(IOk r)
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
		public async Task<IR<List<T>>> ExecuteQueryAsync(IOk r)
		{
			// Get query
			var query = unitOfWork.Adapter.Retrieve(parts);

			// Execute and return
			return await unitOfWork.QueryAsync<T>(r, query, parts.Parameters) switch
			{
				IOkV<IEnumerable<T>> x => x.OkV(x.Value.ToList()),
				{ } x => x.Error<List<T>>()
			};
		}

		/// <inheritdoc/>
		public async Task<IR<PagedList<T>>> ExecuteQueryAsync(IOk r, long page)
		{
			// Run chain
			return r
				.Link().MapAsync(getCount).Await()
				.Link().Run(getPagingValues)
				.Link().MapAsync(getItems).Await()
				.Link().Map(getPagedList);

			// Get the count
			async Task<IR<long>> getCount(IOk r) => await GetCountAsync(r);

			// Get paging values
			void getPagingValues(IOkV<long> r)
			{
				// Create values object
				var values = new PagingValues(r.Value, page, parts.Limit ?? Defaults.PagingValues.ItemsPer);

				// Set the OFFSET and LIMIT values
				parts.Offset = (values.Page - 1) * values.ItemsPer;
				parts.Limit = values.ItemsPer;
			}

			// Get the items
			async Task<IR<List<T>>> getItems(IOkV<long> r)
			{
				return await ExecuteQueryAsync(r.Ok()).ConfigureAwait(false);
			}

			// Convert to a paged list
			IR<PagedList<T>> getPagedList(IOkV<List<T>> r)
			{
				var list = new PagedList<T>(r.Value);
				return r.OkV(list);
			}
		}
	}
}
