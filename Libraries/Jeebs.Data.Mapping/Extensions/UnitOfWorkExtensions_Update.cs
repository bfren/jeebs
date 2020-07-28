using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace Jeebs.Data
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
		/// <param name="w">IUnitOfWork</param>
		/// <param name="r">Result</param>
		public static IR<bool> Update<T>(this IUnitOfWork w, IOkV<T> r)
			where T : class, IEntity
		{
			try
			{
				lock (_)
				{
					// Perform the update
					var result = r.Value switch
					{
						IEntityWithVersion e => UpdateWithVersion(w, r.OkV(e)),
						_ => UpdateWithoutVersion(w, r)
					};

					// Add debug and result messages
					var message = new Jm.Data.UpdateMsg(typeof(T), r.Value.Id);
					w.LogDebug(message);
					result.Messages.Add(message);

					// Return result
					return result;
				}
			}
			catch (Exception ex)
			{
				return r.False(new Jm.Data.UpdateExceptionMsg(ex, typeof(T), r.Value.Id));
			}
			finally
			{
				w.Rollback();
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
			w.LogQuery(nameof(UpdateWithVersion), query, poco);

			// Execute and return
			var rowsAffected = w.Connection.Execute(query, param: poco, transaction: w.Transaction);
			if (rowsAffected == 1)
			{
				return r.True();
			}

			return r.False(new Jm.Data.UpdateErrorMsg(typeof(T), poco.Id));
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
			w.LogQuery(nameof(UpdateWithoutVersion), query, poco);

			// Execute and return
			var rowsAffected = w.Connection.Execute(query, param: poco, transaction: w.Transaction);
			if (rowsAffected == 1)
			{
				return r.True();
			}

			return r.False(new Jm.Data.UpdateErrorMsg(typeof(T), poco.Id));
		}
	}
}
