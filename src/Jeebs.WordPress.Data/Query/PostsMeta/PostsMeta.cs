// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsMeta{TEntity}"/>
		public abstract record PostsMeta<TEntity> : IQueryPostsMeta<TEntity>
		where TEntity : WpPostMetaEntity
		{
			private readonly IWpDbQuery query;

			/// <summary>
			/// Inject dependencies
			/// </summary>
			/// <param name="query">IWpDbQuery</param>
			internal PostsMeta(IWpDbQuery query) =>
				this.query = query;

			/// <inheritdoc/>
			public Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(IQueryPostsMeta<TEntity>.GetOptions opt)
				where TModel : IWithId =>
				Return(
					() => opt(new(query.Db)),
					e => new Msg.ErrorGettingQueryPostsMetaOptionsMsg(e)
				)
				.Bind(
					x => x.GetParts<TModel>()
				)
				.BindAsync(
					x => query.QueryAsync<TModel>(x)
				);
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get posts meta query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryPostsMetaOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
