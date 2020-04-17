using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Jeebs.Data
{
	/// <summary>
	/// IUnitOfWork extensions - DELETE
	/// </summary>
	public static partial class UnitOfWorkExtensions
	{
		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="poco">Entity to delete</param>
		public static IResult<bool> Delete<T>(this IUnitOfWork w, T poco)
			where T : class, IEntity
		{
			var error = $"Unable to delete {typeof(T)} '{poco.Id}'.";

			try
			{
				// Build query
				var query = w.Adapter.DeleteSingle<T>();
				w.LogQuery(nameof(Delete), query, poco);

				// Execute and return
				var rowsAffected = w.Connection.Execute(query, param: poco, transaction: w.Transaction);
				if (rowsAffected == 1)
				{
					return Result.Success();
				}

				return w.Fail(error);
			}
			catch (Exception ex)
			{
				return w.Fail(ex, error);
			}
		}

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="poco">Entity to delete</param>
		public static async Task<IResult<bool>> DeleteAsync<T>(this IUnitOfWork w, T poco)
			where T : class, IEntity
		{
			var error = $"Unable to delete {typeof(T)} '{poco.Id}'.";

			try
			{
				// Build query
				var query = w.Adapter.DeleteSingle<T>();
				w.LogQuery(nameof(DeleteAsync), query, poco);

				// Execute and return
				var rowsAffected = await w.Connection.ExecuteAsync(query, param: poco, transaction: w.Transaction);
				if (rowsAffected == 1)
				{
					return Result.Success();
				}

				return w.Fail(error);
			}
			catch (Exception ex)
			{
				return w.Fail(ex, error);
			}
		}
	}
}
