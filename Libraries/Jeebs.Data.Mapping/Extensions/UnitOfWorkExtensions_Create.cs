using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Jeebs.Data
{
	/// <summary>
	/// IUnitOfWork extensions - CREATE
	/// </summary>
	public static partial class UnitOfWorkExtensions
	{
		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="poco">Entity object</param>
		/// <returns>Entity (complete with new ID)</returns>
		public static IResult<T> Insert<T>(this IUnitOfWork w, T poco)
			where T : class, IEntity
		{
			// Declare here so accessible outside try...catch
			int newId;

			try
			{
				// Build query
				var query = w.Adapter.CreateSingleAndReturnId<T>();
				w.LogQuery(nameof(Insert), query, poco);

				// Insert and capture new ID
				newId = w.Connection.ExecuteScalar<int>(query, param: poco, transaction: w.Transaction);
			}
			catch (Exception ex)
			{
				return w.Fail<T>(ex, $"Unable to insert {typeof(T)}.");
			}

			// If newId is still 0, rollback changes - something went wrong
			if (newId == 0)
			{
				return w.Fail<T>($"Unable to retrieve ID of inserted {typeof(T)}.");
			}

			// Retrieve fresh POCO with inserted ID
			return w.Single<T>(newId);
		}

		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="poco">Entity object</param>
		/// <returns>Entity (complete with new ID)</returns>
		public static async Task<IResult<T>> InsertAsync<T>(this IUnitOfWork w, T poco)
			where T : class, IEntity
		{
			// Declare here so accessible outside try...catch
			int newId;

			try
			{
				// Build query
				var query = w.Adapter.CreateSingleAndReturnId<T>();
				w.LogQuery(nameof(InsertAsync), query, poco);

				// Insert and capture new ID
				newId = await w.Connection.ExecuteScalarAsync<int>(query, param: poco, transaction: w.Transaction);
			}
			catch (Exception ex)
			{
				return w.Fail<T>(ex, $"Unable to insert {typeof(T)}.");
			}

			// If newId is still 0, rollback changes - something went wrong
			if (newId == 0)
			{
				return w.Fail<T>($"Unable to retrieve ID of inserted {typeof(T)}.");
			}

			return await w.SingleAsync<T>(newId);
		}
	}
}
