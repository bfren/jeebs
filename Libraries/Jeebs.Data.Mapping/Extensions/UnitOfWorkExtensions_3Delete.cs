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
					async x =>
					{
						// Build query
						var query = w.Adapter.DeleteSingle<T>();
						w.Log.Message(new Msg.DeleteQueryMsg<T>(method, query, x));

						// Execute
						var rowsAffected = await execute(query, x, w.Transaction).ConfigureAwait(false);
						if (rowsAffected == 1)
						{
							return True;
						}

						return None<bool>(new Msg.DeleteErrorMsg<T>(method, x.Id));
					},
					e => new Msg.DeleteExceptionMsg<T>(method, entity.Id, e)
				);
		}
	}

	namespace UnitOfWorkExtensionsMsg
	{
		/// <summary>Something went wrong deleting the entity</summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
		/// <param name="Id">Entity ID being requested</param>
		public sealed record DeleteErrorMsg<T>(string Method, long Id) :
			LogMsg(LogLevel.Warning, "{Method} {Id}")
		{
			/// <inheritdoc/>
			public override Func<object[]> Args =>
				() => new object[] { Method, Id };
		}

		/// <summary>Error deleting entity</summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
		/// <param name="Id">Entity ID being requested</param>
		/// <param name="Exception">Caught exception</param>
		public sealed record DeleteExceptionMsg<T>(string Method, long Id, Exception Exception) :
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
		public sealed record DeleteQueryMsg<T>(string Method, string Query, T Parameters) :
			LogMsg(LogLevel.Debug, "{Method} {Query} ({@Parameters})")
		{
			/// <inheritdoc/>
			public override Func<object[]> Args =>
				() => new object[] { Method, Query, Parameters ?? new object() };
		}
	}
}
