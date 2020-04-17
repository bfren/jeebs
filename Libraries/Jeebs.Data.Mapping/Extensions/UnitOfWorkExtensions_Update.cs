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
		/// <param name="poco">Entity object</param>
		public static IResult<bool> Update<T>(this IUnitOfWork w, T poco)
			where T : class, IEntity
		{
			lock (_)
			{
				if (poco is IEntityWithVersion pocoWithVersion)
				{
					return UpdateWithVersion(w, pocoWithVersion);
				}
				else
				{
					return UpdateWithoutVersion(w, poco);
				}
			}
		}

		/// <summary>
		/// Update using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="poco">Object</param>
		private static IResult<bool> UpdateWithVersion<T>(IUnitOfWork w, T poco)
			where T : class, IEntityWithVersion
		{
			var currentVersion = poco.Version;
			var error = $"Unable to update {typeof(T)} '{poco.Id}'.";

			try
			{
				// Build query and increase the version number
				var query = w.Adapter.UpdateSingle<T>();
				poco.Version++;
				w.LogQuery(nameof(UpdateWithVersion), query, poco);

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
		/// Update without using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="poco">Object</param>
		private static IResult<bool> UpdateWithoutVersion<T>(IUnitOfWork w, T poco)
			where T : class, IEntity
		{
			var error = $"Unable to update {typeof(T)} '{poco.Id}'.";

			try
			{
				// Build query
				var query = w.Adapter.UpdateSingle<T>();
				w.LogQuery(nameof(UpdateWithoutVersion), query, poco);

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
	}
}
