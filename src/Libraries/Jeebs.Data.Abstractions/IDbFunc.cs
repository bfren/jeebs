// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Database functions for an entity type, including CRUD and custom queries
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">Strong ID type</typeparam>
	public interface IDbFunc<TEntity, TId>
		where TEntity : IEntity
		where TId : StrongId
	{
		#region Custom Queries

		/// <summary>
		/// Retrieve a single item matching all <paramref name="predicates"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="predicates">Predicates (matched using AND)</param>
		Task<Option<TModel>> QuerySingleAsync<TModel>(
			params (Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		);

		/// <summary>
		/// Retrieve items matching all <paramref name="predicates"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="predicates">Predicates (matched using AND)</param>
		Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			params (Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		);

		#endregion

		#region CRUD Queries

		/// <summary>
		/// Create an entity
		/// </summary>
		/// <param name="model">Model with values to use when creating</param>
		Task<Option<TId>> CreateAsync<TModel>(TModel model);

		/// <summary>
		/// Retrieve an entity
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="id">Entity ID</param>
		Task<Option<TModel>> RetrieveAsync<TModel>(TId id);

		/// <summary>
		/// Update an entity with the values in <paramref name="model"/>
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="model">Model with updated values</param>
		Task<Option<bool>> UpdateAsync<TModel>(TModel model)
			where TModel : IWithId;

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <param name="id">Entity ID</param>
		Task<Option<bool>> DeleteAsync(TId id);

		#endregion
	}
}
