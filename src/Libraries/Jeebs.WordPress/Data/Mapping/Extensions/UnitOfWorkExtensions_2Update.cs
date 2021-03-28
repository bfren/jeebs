// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Logging;
using static F.OptionF;

namespace Jeebs.WordPress.Data.Mapping
{
	/// <summary>
	/// IUnitOfWork extensions - UPDATE
	/// </summary>
	public static partial class UnitOfWorkExtensions
	{
		/// <summary>
		/// Update an object
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="entity">Entity</param>
		public static Option<bool> Update<T>(this IUnitOfWork @this, T entity)
			where T : class, IEntity =>
			entity switch
			{
				IEntityWithVersion e =>
					UpdateWithVersion(@this, e),

				{ } e =>
					UpdateWithoutVersion(@this, e)
			};

		/// <summary>
		/// Update using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="entity">Entity to update</param>
		private static Option<bool> UpdateWithVersion<T>(IUnitOfWork w, T entity)
			where T : class, IEntityWithVersion =>
			Return(
				() => w.Adapter.UpdateSingle<T>(),
				e => new Msg.GetUpdateQueryExceptionMsg<T>(nameof(UpdateWithVersion), entity.Id, e)
			)
			.AuditSwitch(
				some: x =>
				{
					entity.Version++;
					w.Log.Message(new Msg.AuditUpdateQueryMsg<T>(nameof(UpdateWithVersion), x, entity));
				}
			)
			.Bind(
				x => w.Execute(x, entity)
			)
			.Bind(
				x => x switch
				{
					1 =>
						True,

					_ =>
						None<bool>(new Msg.UpdateErrorMsg<T>(nameof(UpdateWithVersion), entity.Id))
				}
			);

		/// <summary>
		/// Update without using versioning
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="entity">Entity to update</param>
		private static Option<bool> UpdateWithoutVersion<T>(IUnitOfWork w, T entity)
			where T : class, IEntity =>
			Return(
				() => w.Adapter.UpdateSingle<T>(),
				e => new Msg.GetUpdateQueryExceptionMsg<T>(nameof(UpdateWithoutVersion), entity.Id, e)
			)
			.AuditSwitch(
				some: x => w.Log.Message(new Msg.AuditUpdateQueryMsg<T>(nameof(UpdateWithoutVersion), x, entity))
			)
			.Bind(
				x => w.Execute(x, entity)
			)
			.Bind(
				x => x switch
				{
					1 =>
						True,

					_ =>
						None<bool>(new Msg.UpdateErrorMsg<T>(nameof(UpdateWithoutVersion), entity.Id))
				}
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Something went wrong updating the entity</summary>
			/// <typeparam name="T">Entity type</typeparam>
			/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
			/// <param name="Id">Entity ID being updated</param>
			public sealed record UpdateErrorMsg<T>(string Method, long Id) :
				LogMsg(LogLevel.Warning, "{Method} {Id}")
			{
				/// <inheritdoc/>
				public override Func<object[]> Args =>
					() => new object[] { Method, Id };
			}

			/// <summary>Error getting update query</summary>
			/// <typeparam name="T">Entity type</typeparam>
			/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
			/// <param name="Id">Entity ID being updated</param>
			/// <param name="Exception">Caught exception</param>
			public sealed record GetUpdateQueryExceptionMsg<T>(string Method, long Id, Exception Exception) :
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
			public sealed record AuditUpdateQueryMsg<T>(string Method, string Query, T Parameters) :
				LogMsg(LogLevel.Debug, "{Method} {Query} ({@Parameters})")
			{
				/// <inheritdoc/>
				public override Func<object[]> Args =>
					() => new object[] { Method, Query, Parameters ?? new object() };
			}
		}
	}
}
