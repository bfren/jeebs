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
		/// <param name="r">Result object - the value should be the poco to delete</param>
		public static IR<bool> Delete<T>(this IUnitOfWork w, IOkV<T> r)
			where T : class, IEntity
		{
			// Get poco
			var poco = r.Val;

			try
			{
				// Build query
				var query = w.Adapter.DeleteSingle<T>();
				w.LogQuery(nameof(Delete), query, poco);

				// Execute
				var rowsAffected = w.Connection.Execute(query, param: poco, transaction: w.Transaction);
				if (rowsAffected == 1)
				{
					// Add debug and result messages
					var message = new Jm.Data.Retrieve(typeof(T), poco.Id);
					w.LogDebug(message);
					r.Messages.Add(message);

					// Return
					return r.OkSimple();
				}

				return r.ErrorSimple(new Jm.Data.DeleteError(typeof(T), poco.Id));
			}
			catch (Exception ex)
			{
				return r.ErrorSimple(new Jm.Data.DeleteException(ex, typeof(T), poco.Id));
			}
		}

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="r">Result object - the value should be the poco to delete</param>
		public static async Task<IR<bool>> DeleteAsync<T>(this IUnitOfWork w, IOkV<T> r)
			where T : class, IEntity
		{
			// Get poco
			var poco = r.Val;

			try
			{
				// Build query
				var query = w.Adapter.DeleteSingle<T>();
				w.LogQuery(nameof(DeleteAsync), query, poco);

				// Execute and return
				var rowsAffected = await w.Connection.ExecuteAsync(query, param: poco, transaction: w.Transaction).ConfigureAwait(false);
				if (rowsAffected == 1)
				{
					// Add debug and result messages
					var message = new Jm.Data.Retrieve(typeof(T), poco.Id);
					w.LogDebug(message);
					r.Messages.Add(message);

					// Return
					return r.OkSimple();
				}

				return r.ErrorSimple(new Jm.Data.DeleteError(typeof(T), poco.Id));
			}
			catch (Exception ex)
			{
				return r.ErrorSimple(new Jm.Data.DeleteException(ex, typeof(T), poco.Id));
			}
		}
	}
}
