// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jm.Data.Querying.Query;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQuery{T}"/>
	public sealed class Query<T> : IQuery<T>
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		internal IUnitOfWork UnitOfWork { get; }

		/// <summary>
		/// QueryParts
		/// </summary>
		internal IQueryParts Parts { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="parts">IQueryParts</param>
		internal Query(IUnitOfWork unitOfWork, IQueryParts parts) =>
			(UnitOfWork, Parts) = (unitOfWork, parts);

		/// <inheritdoc/>
		public async Task<IR<long>> GetCountAsync(IOk r)
		{
			// Store original SELECT
			var originalSelect = Parts.Select;

			// Alter SELECT
			Parts.Select = UnitOfWork.Adapter.GetSelectCount();

			// Get count query
			var countQuery = UnitOfWork.Adapter.Retrieve(Parts);

			// Execute
			var count = await UnitOfWork.ExecuteScalarAsync<long>(r, countQuery, Parts.Parameters).ConfigureAwait(false);

			// Restore SELECT and return
			Parts.Select = originalSelect;
			return count;
		}

		/// <inheritdoc/>
		public async Task<IR<List<T>>> ExecuteQueryAsync(IOk r)
		{
			// Get query
			var query = UnitOfWork.Adapter.Retrieve(Parts);

			// Execute and return
			var items = await UnitOfWork.QueryAsync<T>(r, query, Parts.Parameters).ConfigureAwait(false);
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
			IR<PagingValues> getPagingValues(IOkV<long> r) =>
				r.OkV(new PagingValues(r.Value, page, Parts.Limit ?? Defaults.PagingValues.ItemsPer));

			// Get the items
			async Task<IR<PagedList<T>>> getItems(IOkV<PagingValues> r)
			{
				// Set the OFFSET and LIMIT values based on the calculated paging values
				Parts.Offset = (r.Value.Page - 1) * r.Value.ItemsPer;
				Parts.Limit = r.Value.ItemsPer;

				// Get query
				var query = UnitOfWork.Adapter.Retrieve(Parts);

				// Execute and return
				var items = await UnitOfWork.QueryAsync<T>(r, query, Parts.Parameters).ConfigureAwait(false);
				return items.Switch(
					x => x.OkV(new PagedList<T>(r.Value, x.Value))
				);
			}
		}

		/// <inheritdoc/>
		public async Task<IR<T>> ExecuteScalarAsync(IOk r)
		{
			// Get query
			var query = UnitOfWork.Adapter.Retrieve(Parts);

			// Execute and return
			return await UnitOfWork.ExecuteScalarAsync<T>(r, query, Parts.Parameters).ConfigureAwait(false);
		}
	}
}
