// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Common.Query;

namespace Jeebs.Data.Common;

/// <summary>
/// Repository for an entity type, including CRUD and custom queries.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TId">StrongId type.</typeparam>
public interface IRepository<TEntity, TId> : Data.IRepository<TEntity, TId>
	where TEntity : IWithId
	where TId : class, IUnion, new()
{
	#region Fluent Queries

	/// <summary>
	/// Start a new fluent query.
	/// </summary>
	IFluentQuery<TEntity, TId> StartFluentQuery();

	#endregion Fluent Queries

	#region CRUD Queries

	/// <summary>
	/// Create an entity.
	/// </summary>
	/// <param name="entity">Entity to create.</param>
	/// <param name="transaction">Database transaction.</param>
	Task<Result<TId>> CreateAsync(TEntity entity, IDbTransaction transaction);

	/// <summary>
	/// Retrieve an entity.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="id">Entity ID.</param>
	/// <param name="transaction">Database transaction.</param>
	Task<Result<TModel>> RetrieveAsync<TModel>(TId id, IDbTransaction transaction);

	/// <summary>
	/// Update an entity with the values in <paramref name="model"/>.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="model">Model with updated values.</param>
	/// <param name="transaction">Database transaction.</param>
	Task<Result<bool>> UpdateAsync<TModel>(TModel model, IDbTransaction transaction)
		where TModel : IWithId;

	/// <summary>
	/// Delete an entity.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="model">Model containing ID of entity to delete (and Version if required).</param>
	/// <param name="transaction">Database transaction.</param>
	Task<Result<bool>> DeleteAsync<TModel>(TModel model, IDbTransaction transaction)
		where TModel : IWithId;

	#endregion CRUD Queries
}
