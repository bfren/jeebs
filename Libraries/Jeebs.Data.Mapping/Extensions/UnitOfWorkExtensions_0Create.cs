// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Dapper;
using Jm.Data.Mapping.Extensions.UnitOfWork;

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
			Option
				.Wrap(entity)
				.Bind(
					x => InsertAndReturnId(@this, x),
					e => new Jm.Data.CreateExceptionMsg(e, typeof(T))
				)
				.Bind(
					x => CheckId<T>(x),
					e => new CheckIdExceptionMsg(e)
				)
				.Bind(
					x => GetFreshPoco<T>(@this, x),
					e => new GetFreshPocoExceptionMsg(e)
				);

		/// <summary>
		/// Create an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="entity">New entity</param>
		/// <returns>Entity (complete with new ID)</returns>
		public static async Task<Option<T>> CreateAsync<T>(this IUnitOfWork @this, T entity)
			where T : class, IEntity =>
			await Option
				.Wrap(entity)
				.BindAsync(
					x => InsertAndReturnIdAsync(@this, x),
					e => new Jm.Data.CreateExceptionMsg(e, typeof(T))
				)
				.BindAsync(
					x => CheckId<T>(x),
					e => new CheckIdExceptionMsg(e)
				)
				.BindAsync(
					x => GetFreshPocoAsync<T>(@this, x),
					e => new GetFreshPocoExceptionMsg(e)
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
			w.Log.Message(new Jm.Data.QueryMsg(nameof(InsertAndReturnId), query, entity));

			// Insert and capture new ID
			return w.Connection.ExecuteScalar<long>(query, param: entity, transaction: w.Transaction);
		}

		/// <summary>
		/// Perform an INSERT operation and return the ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="entity">Entity to insert</param>
		private static async Task<Option<long>> InsertAndReturnIdAsync<T>(IUnitOfWork w, T entity)
			where T : IEntity
		{
			// Build query
			var query = w.Adapter.CreateSingleAndReturnId<T>();
			w.Log.Message(new Jm.Data.QueryMsg(nameof(InsertAndReturnIdAsync), query, entity));

			// Insert and capture new ID
			return await w.Connection.ExecuteScalarAsync<long>(query, param: entity, transaction: w.Transaction).ConfigureAwait(false);
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
				return Option.None<long>(new Jm.Data.CreateErrorMsg(typeof(T)));
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
			w.Log.Message(new Jm.Data.CreateMsg(typeof(T), id));

			// Get fresh poco
			return Single<T>(w, id);
		}

		/// <summary>
		/// Get a fresh POCO from the database using the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="id">Entity ID</param>
		private static async Task<Option<T>> GetFreshPocoAsync<T>(IUnitOfWork w, long id)
			where T : class, IEntity
		{
			// Add create message
			w.Log.Message(new Jm.Data.CreateMsg(typeof(T), id));

			// Get fresh poco
			return await SingleAsync<T>(w, id);
		}
	}
}
