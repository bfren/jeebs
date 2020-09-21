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
			var items = await unitOfWork.QueryAsync<T>(r, query, parts.Parameters).ConfigureAwait(false);
			return items.Switch(
				x => x.OkV(x.Value.ToList())
			);
		}

		/// <inheritdoc/>
		public async Task<IR<PagedList<T>>> ExecuteQueryAsync(IOk r, long page)
		{
			// Run chain
			return r
				.Link()
					.Handle().With<GetCountExceptionMsg>()
					.MapAsync(GetCountAsync).Await()
				.Link()
					.Handle().With<GetPagingValuesExceptionMsg>()
					.Map(getPagingValues)
				.Link()
					.Handle().With<GetItemsExceptionMsg>()
					.MapAsync(getItems).Await();

			// Get paging values
			IR<PagingValues> getPagingValues(IOkV<long> r)
				=> r.OkV(new PagingValues(r.Value, page, parts.Limit ?? Defaults.PagingValues.ItemsPer));
			
			// Get the items
			async Task<IR<PagedList<T>>> getItems(IOkV<PagingValues> r)
			{
				// Set the OFFSET and LIMIT values based on the calculated paging values
				parts.Offset = (r.Value.Page - 1) * r.Value.ItemsPer;
				parts.Limit = r.Value.ItemsPer;

				// Get query
				var query = unitOfWork.Adapter.Retrieve(parts);

				// Execute and return
				var items = await unitOfWork.QueryAsync<T>(r, query, parts.Parameters).ConfigureAwait(false);
				return items.Switch(
					x => x.OkV(new PagedList<T>(r.Value, x.Value))
				);
			}
		}
	}
}
