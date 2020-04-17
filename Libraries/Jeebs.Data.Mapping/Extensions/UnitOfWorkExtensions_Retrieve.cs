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
		/// <param name="id">Entity ID</param>
		public static IResult<T> Single<T>(this IUnitOfWork w, int id)
			where T : class, IEntity
		{
			try
			{
				// Build query
				var query = w.Adapter.RetrieveSingleById<T>();
				w.LogQuery(nameof(Single), query, new { id });

				// Execute and return
				var result = w.Connection.QuerySingle<T>(query, param: new { id }, transaction: w.Transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return w.Fail<T>(ex, $"An error occured while retrieving {typeof(T)} with ID '{id}'.");
			}
		}

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="id">Entity ID</param>
		private static async Task<IResult<T>> SingleAsync<T>(this IUnitOfWork w, int id)
			where T : class, IEntity
		{
			try
			{
				// Build query
				var query = w.Adapter.RetrieveSingleById<T>();
				w.LogQuery(nameof(SingleAsync), query, new { id });

				// Execute and return
				var result = await w.Connection.QuerySingleAsync<T>(query, param: new { id }, transaction: w.Transaction);
				return Result.Success(result);
			}
			catch (Exception ex)
			{
				return w.Fail<T>(ex, $"An error occured while retrieving {typeof(T)} with ID '{id}'.");
			}
		}
	}
}
