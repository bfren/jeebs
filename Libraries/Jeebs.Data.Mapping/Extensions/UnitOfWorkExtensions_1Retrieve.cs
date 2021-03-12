// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using static JeebsF.OptionF;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// IUnitOfWork extensions - RETRIEVE
	/// </summary>
	public static partial class UnitOfWorkExtensions
	{
		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="id">Entity ID</param>
		public static Option<T> Single<T>(this IUnitOfWork @this, long id)
			where T : class, IEntity =>
			SingleAsync(
				id,
				@this,
				nameof(Single),
				(q, p, t) => Task.FromResult(@this.Connection.QuerySingle<T>(q, p, t))
			).Result;

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="id">Entity ID</param>
		private static Task<Option<T>> SingleAsync<T>(this IUnitOfWork @this, long id)
			where T : class, IEntity =>
			SingleAsync(
				id,
				@this,
				nameof(SingleAsync),
				(q, p, t) => @this.Connection.QuerySingleAsync<T>(q, p, t)
			);

		private static Task<Option<T>> SingleAsync<T>(long id, IUnitOfWork w, string method, Func<string, object, IDbTransaction, Task<T>> execute)
			where T : class, IEntity
		{
			return Return(id)
				.BindAsync(
					retrievePoco,
					e => new Jm.Data.RetrieveExceptionMsg(e, typeof(T), id)
				);

			// Delete the poco
			async Task<Option<T>> retrievePoco(long id)
			{
				// Build query
				var query = w.Adapter.RetrieveSingleById<T>();
				w.Log.Message(new Jm.Data.QueryMsg(method, query, new { id }));

				// Execute
				var result = await execute(query, new { id }, w.Transaction).ConfigureAwait(false);

				// Add retrieve message
				w.Log.Message(new Jm.Data.RetrieveMsg(typeof(T), id));

				// Return
				return result;
			}
		}
	}
}
