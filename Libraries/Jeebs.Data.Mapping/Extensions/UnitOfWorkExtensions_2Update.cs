using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

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
		private static readonly object _ = new object();

		/// <summary>
		/// Update an object
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="r">Result</param>
		public static IR<bool> Update<T>(this IUnitOfWork @this, IOkV<T> r)
			where T : class, IEntity
		{
			try
			{
				lock (_)
				{
					// Perform the update
					var result = r.Value switch
					{
						IEntityWithVersion e =>
							UpdateWithVersion(@this, r.OkV(e)),

						_ =>
							UpdateWithoutVersion(@this, r)
					};

					// Add update messages
					result.AddMsg(new Jm.Data.UpdateMsg(typeof(T), r.Value.Id));

					// Return result
					return result;
				}
			}
			catch (Exception ex)
			{
				return r.Error<bool>().AddMsg(new Jm.Data.UpdateExceptionMsg(ex, typeof(T), r.Value.Id));
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
		/// <param name="r">Result</param>
		private static IR<bool> UpdateWithVersion<T>(IUnitOfWork w, IOkV<T> r)
			where T : class, IEntityWithVersion
		{
			var poco = r.Value;

			// Build query and increase the version number
			var query = w.Adapter.UpdateSingle<T>();
			poco.Version++;
			r.AddMsg(new Jm.Data.QueryMsg(nameof(UpdateWithVersion), query, poco));

			// Execute and return
			var rowsAffected = w.Connection.Execute(query, param: poco, transaction: w.Transaction);
			if (rowsAffected == 1)
			{
				return r.OkTrue();
			}

			return r.Error<bool>().AddMsg(new Jm.Data.UpdateErrorMsg(typeof(T), poco.Id));
		}

		/// <summary>
		/// Update without using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="r">Result</param>
		private static IR<bool> UpdateWithoutVersion<T>(IUnitOfWork w, IOkV<T> r)
			where T : class, IEntity
		{
			var poco = r.Value;

			// Build query
			var query = w.Adapter.UpdateSingle<T>();
			r.AddMsg(new Jm.Data.QueryMsg(nameof(UpdateWithoutVersion), query, poco));

			// Execute and return
			var rowsAffected = w.Connection.Execute(query, param: poco, transaction: w.Transaction);
			if (rowsAffected == 1)
			{
				return r.OkTrue();
			}

			return r.Error<bool>().AddMsg(new Jm.Data.UpdateErrorMsg(typeof(T), poco.Id));
		}
	}
}
