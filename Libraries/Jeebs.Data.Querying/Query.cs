using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jm.Data.Querying.Query;

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
			=> (this.unitOfWork, this.parts) = (unitOfWork, parts);

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
			var count = await unitOfWork.ExecuteScalarAsync<long>(r, countQuery, parts.Parameters).ConfigureAwait(false);

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
			return await unitOfWork.QueryAsync<T>(r, query, parts.Parameters).ConfigureAwait(false) switch
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
				.Link()
					.Handle().With<GetCountExceptionMsg>()
					.MapAsync(getCount).Await()
				.Link()
					.Handle().With<GetPagingValuesExceptionMsg>()
					.Map(getPagingValues)
				.Link()
					.Handle().With<GetItemsExceptionMsg>()
					.MapAsync(getItems).Await()
				.Link()
					.Handle().With<GetPagedListExceptionMsg>()
					.Map(getPagedList);

			// Get the count
			async Task<IR<long>> getCount(IOk r)
				=> await GetCountAsync(r).ConfigureAwait(false);

			// Get paging values
			IR<PagingValues> getPagingValues(IOkV<long> r)
			{
				// Create values object
				var values = new PagingValues(r.Value, page, parts.Limit ?? Defaults.PagingValues.ItemsPer);

				// Set the OFFSET and LIMIT values
				parts.Offset = (values.Page - 1) * values.ItemsPer;
				parts.Limit = values.ItemsPer;

				return r.OkV(values);
			}

			// Get the items
			async Task<IR<(List<T>, PagingValues)>> getItems(IOkV<PagingValues> r)
				=> await ExecuteQueryAsync(r).ConfigureAwait(false) switch
				{
					IOkV<List<T>> x => r.OkV((x.Value, r.Value)),
					_ => r.Error<(List<T>, PagingValues)>()
				};

			// Convert to a paged list
			static IR<PagedList<T>> getPagedList(IOkV<(List<T> items, PagingValues values)> r)
				=> r.OkV(new PagedList<T>(r.Value.values, r.Value.items));
		}
	}
}
