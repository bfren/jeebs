// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
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
		/// <param name="entity">Entity to delete</param>
		public static Option<bool> Delete<T>(this IUnitOfWork @this, T entity)
			where T : class, IEntity =>
			Delete(
				entity,
				@this,
				nameof(Delete),
				(q, p, t) => Task.FromResult(@this.Connection.Execute(q, p, t))
			);

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="entity">Entity to delete</param>
		public static async Task<Option<bool>> DeleteAsync<T>(this IUnitOfWork @this, T entity)
			where T : class, IEntity =>
			Delete(
				entity,
				@this,
				nameof(DeleteAsync),
				async (q, p, t) => await @this.Connection.ExecuteAsync(q, p, t)
			);

		private static Option<bool> Delete<T>(T entity, IUnitOfWork w, string method, Func<string, T, IDbTransaction, Task<int>> execute)
			where T : class, IEntity
		{
			return Option
				.Wrap(entity)
				.BindAsync(deletePoco, e => new Jm.Data.DeleteExceptionMsg(e, typeof(T), entity.Id)).Await();

			// Delete the poco
			async Task<Option<bool>> deletePoco(T poco)
			{
				// Build query
				var query = w.Adapter.DeleteSingle<T>();
				w.Log.Message(new Jm.Data.QueryMsg(method, query, poco));

				// Execute
				var rowsAffected = await execute(query, poco, w.Transaction);
				if (rowsAffected == 1)
				{
					// Add delete message
					w.Log.Message(new Jm.Data.DeleteMsg(typeof(T), poco.Id));

					// Return
					return Option.True;
				}

				return Option.None<bool>(new Jm.Data.DeleteErrorMsg(typeof(T), poco.Id));
			}
		}
	}
}
