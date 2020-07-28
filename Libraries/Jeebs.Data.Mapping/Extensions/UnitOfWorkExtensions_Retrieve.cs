using System;
using System.Collections.Generic;
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
		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="r">Result object - the value should be the entity ID</param>
		public static IR<T> Single<T>(this IUnitOfWork w, IOkV<long> r)
			where T : class, IEntity
		{
			// Get id
			var id = r.Value;

			try
			{
				// Build query
				var query = w.Adapter.RetrieveSingleById<T>();
				w.LogQuery(nameof(Single), query, new { id });

				// Execute
				var result = w.Connection.QuerySingle<T>(query, param: new { id }, transaction: w.Transaction);

				// Add debug and result messages
				var message = new Jm.Data.RetrieveMsg(typeof(T), id);
				w.LogDebug(message);
				r.Messages.Add(message);

				// Return
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return r.Error<T>().AddMsg(new Jm.Data.RetrieveExceptionMsg(ex, typeof(T), id));
			}
		}

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="r">Result object - the value should be the entity ID</param>
		private static async Task<IR<T>> SingleAsync<T>(this IUnitOfWork w, IOkV<long> r)
			where T : class, IEntity
		{
			// Get id
			var id = r.Value;

			try
			{
				// Build query
				var query = w.Adapter.RetrieveSingleById<T>();
				w.LogQuery(nameof(SingleAsync), query, new { id });

				// Execute and return
				var result = await w.Connection.QuerySingleAsync<T>(query, param: new { id }, transaction: w.Transaction).ConfigureAwait(false);

				// Add debug and result messages
				var message = new Jm.Data.RetrieveMsg(typeof(T), id);
				w.LogDebug(message);
				r.Messages.Add(message);

				// Return
				return r.OkV(result);
			}
			catch (Exception ex)
			{
				return r.Error<T>().AddMsg(new Jm.Data.RetrieveExceptionMsg(ex, typeof(T), id));
			}
		}
	}
}
