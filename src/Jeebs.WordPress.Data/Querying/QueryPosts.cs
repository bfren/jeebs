// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Linq;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data.Querying
{
	/// <inheritdoc cref="IQueryPosts{TEntity}"/>
	public abstract record QueryPosts<TEntity> : IQueryPosts<TEntity>
		where TEntity : WpPostEntity
	{
		private readonly IWpDbQuery query;

		private readonly ILog log;

		internal QueryPosts(IWpDbQuery query, ILog log) =>
			(this.query, this.log) = (query, log);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(Func<QueryPostsOptions<TEntity>, QueryPostsOptions<TEntity>> opt)
		{
			// Get options
			var options = opt(new(query.Db));

			// Get parts
			return from parts in options.GetParts<TModel>()
				   from items in query.QueryAsync<TModel>(parts)
				   select items;
		}

		/// <inheritdoc/>
		public Task<Option<IPagedList<TModel>>> ExecuteAsync<TModel>(long page, Func<QueryPostsOptions<TEntity>, QueryPostsOptions<TEntity>> opt)
		{
			// Get options
			var options = opt(new(query.Db));

			// Get parts
			return from parts in options.GetParts<TModel>()
				   from items in query.QueryAsync<TModel>(page, parts)
				   select items;
		}
	}
}
