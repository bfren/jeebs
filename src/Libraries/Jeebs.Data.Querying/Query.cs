// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static F.OptionF;

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
		public Task<Option<long>> GetCountAsync() =>
			Return(
				() => UnitOfWork.Adapter.RetrieveCount(Parts),
				e => new Msg.GetRetrieveCountQueryExceptionMsg(e)
			)
			.BindAsync(
				x => UnitOfWork.ExecuteScalarAsync<long>(x, Parts.Parameters)
			);

		/// <inheritdoc/>
		public Task<Option<List<T>>> ExecuteQueryAsync() =>
			Return(
				() => UnitOfWork.Adapter.Retrieve(Parts),
				e => new Msg.GetRetrieveQueryExceptionMsg(e)
			)
			.BindAsync(
				x => UnitOfWork.QueryAsync<T>(x, Parts.Parameters)
			)
			.MapAsync(
				x => x.ToList(),
				DefaultHandler
			);

		/// <inheritdoc/>
		public Task<Option<IPagedList<T>>> ExecuteQueryAsync(long page) =>
			GetCountAsync()
			.MapAsync(
				x => new PagingValues(x, page, Parts.Limit ?? Defaults.PagingValues.ItemsPer),
				e => new Msg.CreatePagingValuesExceptionMsg(e)
			)
			.BindAsync(
				pagingValues =>
				{
					// Set the OFFSET and LIMIT values based on the calculated paging values
					Parts.Offset = (pagingValues.Page - 1) * pagingValues.ItemsPer;
					Parts.Limit = pagingValues.ItemsPer;

					// Execute and return
					return
						Return(
							() => UnitOfWork.Adapter.Retrieve(Parts),
							e => new Msg.GetPagedRetrieveQueryExceptionMsg(e)
						)
						.BindAsync(
							x => UnitOfWork.QueryAsync<T>(x, Parts.Parameters)
						)
						.MapAsync(
							x => (IPagedList<T>)new PagedList<T>(pagingValues, x),
							DefaultHandler
						);
				}
			);

		/// <inheritdoc/>
		public Task<Option<T>> ExecuteScalarAsync() =>
			Return(
				() => UnitOfWork.Adapter.Retrieve(Parts),
				e => new Msg.GetScalarQueryExceptionMsg(e)
			)
			.BindAsync(
				x => UnitOfWork.ExecuteScalarAsync<T>(x, Parts.Parameters)
			);

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Unable to get count query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetRetrieveCountQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Unable to get retrieve query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetRetrieveQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Unable to get paged retrieve query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetPagedRetrieveQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Unable to get scalar query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetScalarQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Unable to create PagingValues</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record CreatePagingValuesExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
