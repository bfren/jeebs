// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Logging;
using static F.OptionF;
using Msg = Jeebs.Data.Mapping.UnitOfWorkExtensionsMsg;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// IUnitOfWork extensions - CREATE
	/// </summary>
	public static partial class UnitOfWorkExtensions
	{
		/// <summary>
		/// Create an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="entity">New entity</param>
		/// <returns>Entity (complete with new ID)</returns>
		public static Option<T> Create<T>(this IUnitOfWork @this, T entity)
			where T : class, IEntity =>

			Return(entity)
				.Bind(
					x => InsertAndReturnId(@this, x),
					e => new Msg.CreateExceptionMsg<T>(nameof(Create), e)
				)
				.Bind(
					CheckId<T>
				)
				.Bind(
					x => GetFreshPoco<T>(@this, x),
					e => new Msg.RetrieveFreshExceptionMsg<T>(nameof(Create), e)
				);

		/// <summary>
		/// Create an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="entity">New entity</param>
		/// <returns>Entity (complete with new ID)</returns>
		public static Task<Option<T>> CreateAsync<T>(this IUnitOfWork @this, T entity)
			where T : class, IEntity =>

			Return(entity)
				.BindAsync(
					x => InsertAndReturnIdAsync(@this, x),
					e => new Msg.CreateExceptionMsg<T>(nameof(CreateAsync), e)
				)
				.BindAsync(
					CheckId<T>
				)
				.BindAsync(
					x => GetFreshPocoAsync<T>(@this, x),
					e => new Msg.RetrieveFreshExceptionMsg<T>(nameof(Create), e)
				);

		/// <summary>
		/// Perform an INSERT operation and return the ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="entity">Entity to insert</param>
		private static Option<long> InsertAndReturnId<T>(IUnitOfWork w, T entity)
			where T : IEntity
		{
			// Build query
			var query = w.Adapter.CreateSingleAndReturnId<T>();
			w.Log.Message(new Msg.CreateQueryMsg<T>(nameof(InsertAndReturnId), query, entity));

			// Insert and capture new ID
			return w.ExecuteScalar<long>(query, entity);
		}

		/// <summary>
		/// Perform an INSERT operation and return the ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="entity">Entity to insert</param>
		private static Task<Option<long>> InsertAndReturnIdAsync<T>(IUnitOfWork w, T entity)
			where T : IEntity
		{
			// Build query
			var query = w.Adapter.CreateSingleAndReturnId<T>();
			w.Log.Message(new Msg.CreateQueryMsg<T>(nameof(InsertAndReturnIdAsync), query, entity));

			// Insert and capture new ID
			return w.ExecuteScalarAsync<long>(query, entity);
		}

		/// <summary>
		/// Check if the New ID is 0, and if so rollback changes - something went wrong
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		private static Option<long> CheckId<T>(long id)
		{
			// Check ID and return error
			if (id == 0)
			{
				return None<long>(new Msg.CreateErrorMsg<T>());
			}

			// Continue
			return id;
		}

		/// <summary>
		/// Get a fresh POCO from the database using the new ID
		/// </summary>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="id">Entity ID</param>
		private static Option<T> GetFreshPoco<T>(IUnitOfWork w, long id)
			where T : class, IEntity
		{
			// Add create message
			w.Log.Message(new Msg.RetrieveFreshMsg<T>(nameof(GetFreshPoco), id));

			// Get fresh poco
			return Single<T>(w, id);
		}

		/// <summary>
		/// Get a fresh POCO from the database using the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="id">Entity ID</param>
		private static Task<Option<T>> GetFreshPocoAsync<T>(IUnitOfWork w, long id)
			where T : class, IEntity
		{
			// Add create message
			w.Log.Message(new Msg.RetrieveFreshMsg<T>(nameof(GetFreshPocoAsync), id));

			// Get fresh poco
			return SingleAsync<T>(w, id);
		}
	}

	namespace UnitOfWorkExtensionsMsg
	{
		/// <summary>Something went wrong inserting the entity and ID returned was 0</summary>
		/// <typeparam name="T">Entity type</typeparam>
		public sealed record CreateErrorMsg<T>() : IMsg { }

		/// <summary>Error inserting entity</summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
		/// <param name="Exception">Caught exception</param>
		public sealed record CreateExceptionMsg<T>(string Method, Exception Exception) :
			ExceptionMsg(Exception, "{Method}")
		{
			/// <inheritdoc/>
			public override Func<object[]> Args =>
				() => new object[] { Method };
		}

		/// <summary>Query message</summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
		/// <param name="Query">Query text</param>
		/// <param name="Parameters">Query parameters</param>
		public sealed record CreateQueryMsg<T>(string Method, string Query, T Parameters) :
			LogMsg(LogLevel.Debug, "{Method} {Query} ({@Parameters})")
		{
			/// <inheritdoc/>
			public override Func<object[]> Args =>
				() => new object[] { Method, Query, Parameters ?? new object() };
		}

		/// <summary>Error retrieving fresh entity</summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
		/// <param name="Exception">Caught exception</param>
		public sealed record RetrieveFreshExceptionMsg<T>(string Method, Exception Exception) :
			ExceptionMsg(Exception, "{Method}")
		{
			/// <inheritdoc/>
			public override Func<object[]> Args =>
				() => new object[] { Method };
		}

		/// <summary>Query message</summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="Method">The name of the UnitOfWork extension method executing this query</param>
		/// <param name="Id">The ID of the entity being requested</param>
		public sealed record RetrieveFreshMsg<T>(string Method, long Id) :
			LogMsg(LogLevel.Debug, "{Method} {Id}")
		{
			/// <inheritdoc/>
			public override Func<object[]> Args =>
				() => new object[] { Method, Id };
		}
	}
}
