// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;

namespace Jeebs.Data
{
	/// <summary>
	/// Repository for an entity type, including CRUD and custom queries
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">StrongId type</typeparam>
	public interface IRepository<TEntity, TId>
		where TEntity : IWithId
		where TId : IStrongId
	{
		/// <inheritdoc cref="IDb.UnitOfWork"/>
		IUnitOfWork UnitOfWork { get; }

		#region Fluent Queries

		/// <summary>
		/// Start a new fluent query
		/// </summary>
		IQueryFluent<TEntity, TId> StartFluentQuery();

		#endregion

		#region Custom Queries

		/// <summary>
		/// Retrieve items matching all <paramref name="predicates"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="predicates">Predicates (matched using AND)</param>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			params (Expression<Func<TEntity, object>>, Compare, object)[] predicates
		);

		/// <summary>
		/// Retrieve a single item matching all <paramref name="predicates"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="predicates">Predicates (matched using AND)</param>
		Task<Option<TModel>> QuerySingleAsync<TModel>(
			params (Expression<Func<TEntity, object>>, Compare, object)[] predicates
		);

		#endregion

		#region CRUD Queries

		/// <inheritdoc cref="CreateAsync(TEntity, IDbTransaction)"/>
		Task<Option<TId>> CreateAsync(TEntity entity);

		/// <summary>
		/// Create an entity
		/// </summary>
		/// <param name="entity">Entity to create</param>
		/// <param name="transaction">Database transaction</param>
		Task<Option<TId>> CreateAsync(TEntity entity, IDbTransaction transaction);

		/// <inheritdoc cref="RetrieveAsync{TModel}(TId, IDbTransaction)"/>
		Task<Option<TModel>> RetrieveAsync<TModel>(TId id);

		/// <summary>
		/// Retrieve an entity
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <param name="transaction">Database transaction</param>
		Task<Option<TModel>> RetrieveAsync<TModel>(TId id, IDbTransaction transaction);

		/// <inheritdoc cref="UpdateAsync{TModel}(TModel, IDbTransaction)"/>
		Task<Option<bool>> UpdateAsync<TModel>(TModel model)
			where TModel : IWithId;

		/// <summary>
		/// Update an entity with the values in <paramref name="model"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="model">Model with updated values</param>
		/// <param name="transaction">Database transaction</param>
		Task<Option<bool>> UpdateAsync<TModel>(TModel model, IDbTransaction transaction)
			where TModel : IWithId;

		/// <inheritdoc cref="DeleteAsync(TId, IDbTransaction)"/>
		Task<Option<bool>> DeleteAsync(TId id);

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <param name="id">Entity ID</param>
		/// <param name="transaction">Database transaction</param>
		Task<Option<bool>> DeleteAsync(TId id, IDbTransaction transaction);

		#endregion
	}
}
