// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
using System.Linq.Expressions;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Database client
	/// </summary>
	public interface IDbClient
	{
		/// <summary>
		/// Return a new database connection
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		IDbConnection Connect(string connectionString);

		#region Custom Queries

		/// <summary>
		/// Return a query to retrieve a list of entities that match all the specified parameters
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TModel">Return model type</typeparam>
		/// <param name="predicates">Predicates (matched using AND)</param>
		Option<(string query, IQueryParameters param)> GetQuery<TEntity, TModel>(
			(Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		)
			where TEntity : IEntity;

		#endregion

		#region CRUD Queries

		/// <summary>
		/// Return a query to create an entity
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		Option<string> GetCreateQuery<TEntity>()
			where TEntity : IEntity;

		/// <summary>
		/// Return a query to retrieve a single entity by ID
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TModel">Return model type</typeparam>
		/// <param name="id">Entity ID</param>
		Option<string> GetRetrieveQuery<TEntity, TModel>(long id)
			where TEntity : IEntity;

		/// <summary>
		/// Return a query to update a single entity
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TModel">Return model type</typeparam>
		/// <param name="id">Entity ID</param>
		Option<string> GetUpdateQuery<TEntity, TModel>(long id)
			where TEntity : IEntity;

		/// <summary>
		/// Return a query to delete a single entity by ID
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		Option<string> GetDeleteQuery<TEntity>(long id)
			where TEntity : IEntity;

		#endregion
	}
}
