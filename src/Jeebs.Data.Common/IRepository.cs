// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Base = Jeebs.Data.Repository;

namespace Jeebs.Data.Common;

/// <summary>
/// Repository for an entity type, including CRUD and custom queries.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TId">StrongId type.</typeparam>
public interface IRepository<TEntity, TId> : Base.IRepository<TEntity, TId>
	where TEntity : IWithId
	where TId : class, IMonad, new()
{
	/// <summary>
	/// Create an entity.
	/// </summary>
	/// <param name="entity">Entity to create.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	Task<Result<TId>> CreateAsync(TEntity entity, IUnitOfWork unitOfWork);

	/// <summary>
	/// Retrieve an entity.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="id">Entity ID.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	Task<Result<TModel>> RetrieveAsync<TModel>(TId id, IUnitOfWork unitOfWork);

	/// <summary>
	/// Update an entity with the values in <paramref name="model"/>.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="model">Model with updated values.</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	Task<Result<bool>> UpdateAsync<TModel>(TModel model, IUnitOfWork unitOfWork)
		where TModel : IWithId;

	/// <summary>
	/// Delete an entity.
	/// </summary>
	/// <typeparam name="TModel">Model type.</typeparam>
	/// <param name="model">Model containing ID of entity to delete (and Version if required).</param>
	/// <param name="unitOfWork">Unit of Work.</param>
	Task<Result<bool>> DeleteAsync<TModel>(TModel model, IUnitOfWork unitOfWork)
		where TModel : IWithId;
}
