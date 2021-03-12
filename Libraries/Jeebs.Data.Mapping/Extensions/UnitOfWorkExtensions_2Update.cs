// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Dapper;
using static F.OptionF;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// IUnitOfWork extensions - UPDATE
	/// </summary>
	public static partial class UnitOfWorkExtensions
	{
		/// <summary>
		/// Provides thread-safe locking
		/// </summary>
		private static readonly object _ = new();

		/// <summary>
		/// Update an object
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="entity">Entity</param>
		public static Option<bool> Update<T>(this IUnitOfWork @this, T entity)
			where T : class, IEntity
		{
			try
			{
				lock (_)
				{
					// Perform the update
					var result = entity switch
					{
						IEntityWithVersion e =>
							UpdateWithVersion(@this, e),

						{ } e =>
							UpdateWithoutVersion(@this, e)
					};

					// Add update messages
					@this.Log.Message(new Jm.Data.UpdateMsg(typeof(T), entity.Id));

					// Return result
					return result;
				}
			}
			catch (Exception ex)
			{
				return None<bool>(new Jm.Data.UpdateExceptionMsg(ex, typeof(T), entity.Id));
			}
			finally
			{
				@this.Rollback();
			}
		}

		/// <summary>
		/// Update using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="entity">Entity to update</param>
		private static Option<bool> UpdateWithVersion<T>(IUnitOfWork w, T entity)
			where T : class, IEntityWithVersion
		{
			// Build query and increase the version number
			var query = w.Adapter.UpdateSingle<T>();
			entity.Version++;
			w.Log.Message(new Jm.Data.QueryMsg(nameof(UpdateWithVersion), query, entity));

			// Execute and return
			var rowsAffected = w.Connection.Execute(query, param: entity, transaction: w.Transaction);
			if (rowsAffected == 1)
			{
				return True;
			}

			return None<bool>(new Jm.Data.UpdateErrorMsg(typeof(T), entity.Id));
		}

		/// <summary>
		/// Update without using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="entity">Entity to update</param>
		private static Option<bool> UpdateWithoutVersion<T>(IUnitOfWork w, T entity)
			where T : class, IEntity
		{
			// Build query
			var query = w.Adapter.UpdateSingle<T>();
			w.Log.Message(new Jm.Data.QueryMsg(nameof(UpdateWithoutVersion), query, entity));

			// Execute and return
			var rowsAffected = w.Connection.Execute(query, param: entity, transaction: w.Transaction);
			if (rowsAffected == 1)
			{
				return True;
			}

			return None<bool>(new Jm.Data.UpdateErrorMsg(typeof(T), entity.Id));
		}
	}
}
