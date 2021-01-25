using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
		/// <param name="r">Result object - the value should be the poco to insert</param>
		/// <returns>Entity (complete with new ID)</returns>
		public static IR<T> Create<T>(this IUnitOfWork @this, IOkV<T> r)
			where T : class, IEntity =>
			r
				.WithState(@this)
				.Link()
					.Handle().With((r, ex) => r.AddMsg(new Jm.Data.CreateExceptionMsg(ex, typeof(T))))
					.Map(InsertAndReturnId)
				.Link()
					.Handle().With<CheckIdExceptionMsg>()
					.Map(CheckId<T>)
				.Link()
					.Handle().With<GetFreshPocoExceptionMsg>()
					.Map(GetFreshPoco<T>);

		/// <summary>
		/// Create an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="r">Result object - the value should be the poco to insert</param>
		/// <returns>Entity (complete with new ID)</returns>
		public static async Task<IR<T>> CreateAsync<T>(this IUnitOfWork @this, IOkV<T> r)
			where T : class, IEntity =>
			await r
				.WithState(@this)
				.Link()
					.Handle().With((r, ex) => r.AddMsg(new Jm.Data.CreateExceptionMsg(ex, typeof(T))))
					.MapAsync(InsertAndReturnIdAsync).Await()
				.Link()
					.Handle().With<CheckIdExceptionMsg>()
					.Map(CheckId<T>)
				.Link()
					.Handle().With<GetFreshPocoExceptionMsg>()
					.MapAsync(GetFreshPocoAsync<T>);

		/// <summary>
		/// Perform an INSERT operation and return the ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="r">Result object</param>
		private static IR<long, IUnitOfWork> InsertAndReturnId<T>(IOkV<T, IUnitOfWork> r)
			where T : IEntity
		{
			// Get values
			var w = r.State;
			var poco = r.Value;

			// Build query
			var query = w.Adapter.CreateSingleAndReturnId<T>();
			r.AddMsg(new Jm.Data.QueryMsg(nameof(InsertAndReturnId), query, poco));

			// Insert and capture new ID
			var newId = w.Connection.ExecuteScalar<long>(query, param: poco, transaction: w.Transaction);

			// Continue
			return r.OkV(newId);
		}

		/// <summary>
		/// Perform an INSERT operation and return the ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="r">Result object</param>
		private static async Task<IR<long, IUnitOfWork>> InsertAndReturnIdAsync<T>(IOkV<T, IUnitOfWork> r)
			where T : IEntity
		{
			// Get values
			var w = r.State;
			var poco = r.Value;

			// Build query
			var query = w.Adapter.CreateSingleAndReturnId<T>();
			r.AddMsg(new Jm.Data.QueryMsg(nameof(InsertAndReturnIdAsync), query, poco));

			// Insert and capture new ID
			var newId = await w.Connection.ExecuteScalarAsync<long>(query, param: poco, transaction: w.Transaction).ConfigureAwait(false);

			// Continue
			return r.OkV(newId);
		}

		/// <summary>
		/// Check if the New ID is 0, and if so rollback changes - something went wrong
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="r">Result object</param>
		private static IR<long, IUnitOfWork> CheckId<T>(IOkV<long, IUnitOfWork> r)
		{
			// Check ID and return error
			if (r.Value == 0)
			{
				return r.Error().AddMsg(new Jm.Data.CreateErrorMsg(typeof(T)));
			}

			// Continue
			return r;
		}

		/// <summary>
		/// Get a fresh POCO from the database using the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="r">Result object</param>
		private static IR<T, IUnitOfWork> GetFreshPoco<T>(IOkV<long, IUnitOfWork> r)
			where T : class, IEntity
		{
			// Get values
			var w = r.State;

			// Add create message
			r.AddMsg(new Jm.Data.CreateMsg(typeof(T), r.Value));

			// Get fresh poco
			return (IR<T, IUnitOfWork>)Single<T>(w, r);
		}

		/// <summary>
		/// Get a fresh POCO from the database using the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="r">Result object</param>
		private static async Task<IR<T, IUnitOfWork>> GetFreshPocoAsync<T>(IOkV<long, IUnitOfWork> r)
			where T : class, IEntity
		{
			// Get values
			var w = r.State;
			var newId = r.Value;

			// Add create message
			r.AddMsg(new Jm.Data.CreateMsg(typeof(T), newId));

			// Get fresh poco
			return (IR<T, IUnitOfWork>)await w.SingleAsync<T>(r);
		}
	}
}
