// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Database CRUD functions for an entity type
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">Strong ID type</typeparam>
	public interface IDbFunc<TEntity, TId>
		where TEntity : IEntity
		where TId : StrongId
	{
		/// <summary>
		/// Create an entity
		/// </summary>
		/// <param name="entity">Entity to create</param>
		Task<Option<TId>> CreateAsync(TEntity entity);

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
		Task<Option<bool>> UpdateAsync<TModel>(TModel model);

		/// <summary>
		/// Delete an entity
		/// </summary>
		/// <param name="id">Entity ID</param>
		Task<Option<bool>> DeleteAsync(TId id);
	}
}
