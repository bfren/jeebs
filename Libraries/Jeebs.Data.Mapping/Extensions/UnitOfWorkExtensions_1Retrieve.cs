// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Logging;
using static F.OptionF;
using Msg = Jeebs.Data.Mapping.UnitOfWorkExtensionsMsg;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// IUnitOfWork extensions - RETRIEVE
	/// </summary>
	public static partial class UnitOfWorkExtensions
	{
		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="id">Entity ID</param>
		public static Option<T> Single<T>(this IUnitOfWork @this, long id)
			where T : class, IEntity =>
			SingleAsync(
				id,
				@this,
				nameof(Single),
				(q, p, t) => Task.FromResult(@this.Connection.QuerySingle<T>(q, p, t))
			).Result;

		/// <summary>
		/// Get an entity from the database by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="id">Entity ID</param>
		private static Task<Option<T>> SingleAsync<T>(this IUnitOfWork @this, long id)
			where T : class, IEntity =>
			SingleAsync(
				id,
				@this,
				nameof(SingleAsync),
				(q, p, t) => @this.Connection.QuerySingleAsync<T>(q, p, t)
			);

		private static Task<Option<T>> SingleAsync<T>(
			long id, IUnitOfWork w,
			string method,
			Func<string, object, IDbTransaction, Task<T>> execute
		)
			where T : class, IEntity =>
			Return(id)
				.BindAsync<T>(
					async id =>
					{
						// Build query
						var query = w.Adapter.RetrieveSingleById<T>();
						w.Log.Message(new Msg.RetrieveQueryMsg<T>(method, query, new { id }));

						// Execute
						return await execute(query, new { id }, w.Transaction).ConfigureAwait(false);
					},
					e => new Msg.RetrieveExceptionMsg<T>(method, id, e)
				);
	}

	namespace UnitOfWorkExtensionsMsg
	{
		/// <summary>Error retrieving entity</summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
		/// <param name="Id">Entity ID being requested</param>
		/// <param name="Exception">Caught exception</param>
		public sealed record RetrieveExceptionMsg<T>(string Method, long Id, Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>Query message</summary>
		/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
		/// <param name="Query">Query text</param>
		/// <param name="Parameters">Query parameters</param>
		public sealed record RetrieveQueryMsg<T>(string Method, string Query, object? Parameters) : LogMsg(LogLevel.Debug) { }
	}
}
