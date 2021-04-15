// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Query Posts Meta
	/// </summary>
	/// <typeparam name="TEntity">Post Meta Entity type</typeparam>
	public interface IQueryPostsMeta<TEntity> : IQuery<TEntity, WpPostMetaId, Query.PostsMetaOptions<TEntity>>
		where TEntity : WpPostMetaEntity
	{ }
}
