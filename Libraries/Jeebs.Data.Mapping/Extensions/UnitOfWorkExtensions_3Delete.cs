// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using static F.OptionF;

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
			DeleteAsync(
				entity,
				@this,
				nameof(Delete),
				(q, p, t) => Task.FromResult(@this.Connection.Execute(q, p, t))
			).Result;

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="entity">Entity to delete</param>
		public static Task<Option<bool>> DeleteAsync<T>(this IUnitOfWork @this, T entity)
			where T : class, IEntity =>
			DeleteAsync(
				entity,
				@this,
				nameof(DeleteAsync),
				(q, p, t) => @this.Connection.ExecuteAsync(q, p, t)
			);

		private static Task<Option<bool>> DeleteAsync<T>(T entity, IUnitOfWork w, string method, Func<string, T, IDbTransaction, Task<int>> execute)
			where T : class, IEntity
		{
			return Return(entity)
				.BindAsync(
					deletePoco,
					e => new Jm.Data.DeleteExceptionMsg(e, typeof(T), entity.Id)
				);

			// Delete the poco
			async Task<Option<bool>> deletePoco(T poco)
			{
				// Build query
				var query = w.Adapter.DeleteSingle<T>();
				w.Log.Message(new Jm.Data.QueryMsg(method, query, poco));

				// Execute
				var rowsAffected = await execute(query, poco, w.Transaction).ConfigureAwait(false);
				if (rowsAffected == 1)
				{
					// Add delete message
					w.Log.Message(new Jm.Data.DeleteMsg(typeof(T), poco.Id));

					// Return
					return True;
				}

				return None<bool>(new Jm.Data.DeleteErrorMsg(typeof(T), poco.Id));
			}
		}
	}
}
