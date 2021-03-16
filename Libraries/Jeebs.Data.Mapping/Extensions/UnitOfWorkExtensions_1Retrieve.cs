// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Logging;
using static F.OptionF;

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
			Return(
				() => w.Adapter.RetrieveSingleById<T>(),
				e => new Msg.GetRetrieveQueryExceptionMsg<T>(method, id, e)
			)
			.AuditSwitch(
				some: x => w.Log.Message(new Msg.AuditRetrieveQueryMsg<T>(method, x, new { id }))
			)
			.MapAsync(
				x => execute(x, new { id }, w.Transaction),
				e => new Msg.RetrieveExceptionMsg<T>(method, id, e)
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Error getting retrieve query</summary>
			/// <typeparam name="T">Entity type</typeparam>
			/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
			/// <param name="Id">Entity ID being retrieved</param>
			/// <param name="Exception">Caught exception</param>
			public sealed record GetRetrieveQueryExceptionMsg<T>(string Method, long Id, Exception Exception) :
				ExceptionMsg(Exception, "{Method} {Id}")
			{
				/// <inheritdoc/>
				public override Func<object[]> Args =>
					() => new object[] { Method, Id };
			}

			/// <summary>Error retrieving entity</summary>
			/// <typeparam name="T">Entity type</typeparam>
			/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
			/// <param name="Id">Entity ID being requested</param>
			/// <param name="Exception">Caught exception</param>
			public sealed record RetrieveExceptionMsg<T>(string Method, long Id, Exception Exception) :
				ExceptionMsg(Exception, "{Method} {Id}")
			{
				/// <inheritdoc/>
				public override Func<object[]> Args =>
					() => new object[] { Method, Id };
			}

			/// <summary>Query message</summary>
			/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
			/// <param name="Query">Query text</param>
			/// <param name="Parameters">Query parameters</param>
			public sealed record AuditRetrieveQueryMsg<T>(string Method, string Query, object? Parameters) :
				LogMsg(LogLevel.Debug, "{Method} {Query} ({@Parameters})")
			{
				/// <inheritdoc/>
				public override Func<object[]> Args =>
					() => new object[] { Method, Query, Parameters ?? new object() };
			}
		}
	}
}
