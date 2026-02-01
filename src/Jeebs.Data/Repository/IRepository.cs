// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Data.Repository;

/// <summary>
/// Repository for an entity type, including CRUD and custom queries.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TId">StrongId type.</typeparam>
public partial interface IRepository<TEntity, TId>
	where TEntity : IWithId
	where TId : class, IUnion, new()
{

	#region Fluent Queries

	/// <summary>
	/// Start a new fluent query.
	/// </summary>
	IFluentQuery<TEntity, TId> Fluent();

	#endregion Fluent Queries

	#region CRUD

	/// <summary>
	/// Create an entity.
	/// </summary>
	/// <param name="entity">Entity to create.</param>
	Task<Result<TId>> CreateAsync(TEntity entity);

	/// <summary>
	/// Retrieve an entity.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="id">Entity ID.</param>
	Task<Result<TModel>> RetrieveAsync<TModel>(TId id);

	/// <summary>
	/// Update an entity with the values in <paramref name="model"/>.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="model">Model with updated values.</param>
	Task<Result<bool>> UpdateAsync<TModel>(TModel model)
		where TModel : IWithId;

	/// <summary>
	/// Delete an entity.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="model">Model containing ID of entity to delete (and Version if required).</param>
	Task<Result<bool>> DeleteAsync<TModel>(TModel model)
		where TModel : IWithId;

	#endregion
}
