using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Jeebs.Data.Mapping
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
		/// <param name="this">IUnitOfWork</param>
		/// <param name="r">Result object - the value should be the poco to delete</param>
		public static IR<bool> Delete<T>(this IUnitOfWork @this, IOkV<T> r)
			where T : class, IEntity =>
			Delete(
				r,
				@this,
				nameof(Delete),
				(q, p, t) => Task.FromResult(@this.Connection.Execute(q, p, t))
			);

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="r">Result object - the value should be the poco to delete</param>
		public static async Task<IR<bool>> DeleteAsync<T>(this IUnitOfWork @this, IOkV<T> r)
			where T : class, IEntity =>
			Delete(
				r,
				@this,
				nameof(DeleteAsync),
				async (q, p, t) => await @this.Connection.ExecuteAsync(q, p, t)
			);

		private static IR<bool> Delete<T>(IOkV<T> r, IUnitOfWork w, string method, Func<string, T, IDbTransaction, Task<int>> execute)
			where T : class, IEntity
		{
			var pocoId = r.Value.Id;
			return r
				.Link()
					.Handle().With((r, ex) => r.AddMsg(new Jm.Data.DeleteExceptionMsg(ex, typeof(T), pocoId)))
					.MapAsync(deletePoco).Await();

			// Delete the poco
			async Task<IR<bool>> deletePoco(IOkV<T> r)
			{
				// Get poco
				var poco = r.Value;

				// Build query
				var query = w.Adapter.DeleteSingle<T>();
				r.AddMsg(new Jm.Data.QueryMsg(method, query, poco));

				// Execute
				var rowsAffected = await execute(query, poco, w.Transaction);
				if (rowsAffected == 1)
				{
					// Add delete message
					r.AddMsg(new Jm.Data.DeleteMsg(typeof(T), poco.Id));

					// Return
					return r.OkTrue();
				}

				return r.Error<bool>().AddMsg(new Jm.Data.DeleteErrorMsg(typeof(T), poco.Id));
			}
		}
	}
}
