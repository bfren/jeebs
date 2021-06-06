// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Repository for an entity type, including CRUD and custom queries
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">StrongId type</typeparam>
	public interface IRepository<TEntity, TId>
		where TEntity : IEntity
		where TId : StrongId
	{
		/// <inheritdoc cref="IDb.UnitOfWork"/>
		IUnitOfWork UnitOfWork { get; }

		#region Custom Queries

		/// <summary>
		/// Retrieve items matching all <paramref name="predicates"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="predicates">Predicates (matched using AND)</param>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			params (Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		);

		/// <summary>
		/// Retrieve a single item matching all <paramref name="predicates"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="predicates">Predicates (matched using AND)</param>
		Task<Option<TModel>> QuerySingleAsync<TModel>(
			params (Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		);

		#endregion

		#region CRUD Queries

		/// <summary>
		/// Create an entity
		/// </summary>
		/// <param name="entity">Entity to create</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<TId>> CreateAsync(TEntity entity, IDbTransaction? transaction = null);

		/// <summary>
		/// Retrieve an entity
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<TModel>> RetrieveAsync<TModel>(TId id, IDbTransaction? transaction = null);

		/// <summary>
		/// Update an entity with the values in <paramref name="model"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="model">Model with updated values</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<bool>> UpdateAsync<TModel>(TModel model, IDbTransaction? transaction = null)
			where TModel : IWithId;

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <param name="id">Entity ID</param>
		/// <param name="transaction">[Optional] Database transaction</param>
		Task<Option<bool>> DeleteAsync(TId id, IDbTransaction? transaction = null);

		#endregion
	}
}
