﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Jeebs.Data
{
	/// <summary>
	/// IUnitOfWork extensions - RETRIEVE
	/// </summary>
	public static partial class UnitOfWorkExtensions
	{
		private static IR<T> Single<T>(IOkV<long> r, IUnitOfWork w, string method, Func<string, object, IDbTransaction, Task<T>> execute)
			where T : class, IEntity
		{
			// Get id
			var id = r.Value;

			return r
				.Link()
					.Handle().With((r, ex) => r.AddMsg(new Jm.Data.RetrieveExceptionMsg(ex, typeof(T), id)))
					.MapAsync(retrievePoco).Await();

			// Delete the poco
			async Task<IR<T>> retrievePoco(IOkV<long> r)
			{
				// Build query
				var query = w.Adapter.RetrieveSingleById<T>();
				w.LogQuery(r, method, query, new { id });

				// Execute
				var result = await execute(query, new { id }, w.Transaction).ConfigureAwait(false);

				// Add retrieve message
				r.AddMsg(new Jm.Data.RetrieveMsg(typeof(T), id));

				// Return
				return r.OkV(result);
			}
		}

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="r">Result object - the value should be the entity ID</param>
		public static IR<T> Single<T>(this IUnitOfWork w, IOkV<long> r)
			where T : class, IEntity
			=> Single(r, w, nameof(Single), (q, p, t) => Task.FromResult(w.Connection.QuerySingle<T>(q, p, t)));

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="r">Result object - the value should be the entity ID</param>
		private static async Task<IR<T>> SingleAsync<T>(this IUnitOfWork w, IOkV<long> r)
			where T : class, IEntity
			=> Single(r, w, nameof(SingleAsync), async (q, p, t) => await w.Connection.QuerySingleAsync<T>(q, p, t).ConfigureAwait(false));
	}
}
