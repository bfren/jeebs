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
		public async Task<Option<long>> GetCountAsync()
		{
			// Store original SELECT
			var originalSelect = Parts.Select;

			// Alter SELECT
			Parts.Select = UnitOfWork.Adapter.GetSelectCount();

			// Get count query
			var countQuery = UnitOfWork.Adapter.Retrieve(Parts);

			// Execute
			var count = await UnitOfWork.ExecuteScalarAsync<long>(countQuery, Parts.Parameters).ConfigureAwait(false);

			// Restore SELECT and return
			Parts.Select = originalSelect;
			return count;
		}

		/// <inheritdoc/>
		public async Task<Option<List<T>>> ExecuteQueryAsync()
		{
			return from items in await getQuery().BindAsync(getItems)
				   select items.ToList();

			// Get query
			Option<string> getQuery() =>
				UnitOfWork.Adapter.Retrieve(Parts);

			// Get items
			async Task<Option<IEnumerable<T>>> getItems(string query) =>
				await UnitOfWork.QueryAsync<T>(query, Parts.Parameters).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public async Task<Option<IPagedList<T>>> ExecuteQueryAsync(long page)
		{
			// Run chain
			return await GetCountAsync()
				.BindAsync(getPagingValues)
				.BindAsync(getItems);

			// Get paging values
			Option<PagingValues> getPagingValues(long count) =>
				new PagingValues(count, page, Parts.Limit ?? Defaults.PagingValues.ItemsPer);

			// Get the items
			async Task<Option<IPagedList<T>>> getItems(PagingValues paging)
			{
				// Set the OFFSET and LIMIT values based on the calculated paging values
				Parts.Offset = (paging.Page - 1) * paging.ItemsPer;
				Parts.Limit = paging.ItemsPer;

				// Get query
				var query = UnitOfWork.Adapter.Retrieve(Parts);

				// Execute and return
				var items = await UnitOfWork.QueryAsync<T>(query, Parts.Parameters).ConfigureAwait(false);
				return items.Map(
					x => (IPagedList<T>)new PagedList<T>(paging, x)
				);
			}
		}

		/// <inheritdoc/>
		public async Task<Option<T>> ExecuteScalarAsync()
		{
			return await getQuery()
				.BindAsync(getValue);

			// Get query
			Option<string> getQuery() =>
				UnitOfWork.Adapter.Retrieve(Parts);

			// Execute query
			async Task<Option<T>> getValue(string query) =>
				await UnitOfWork.ExecuteScalarAsync<T>(query, Parts.Parameters).ConfigureAwait(false);
		}
	}
}
