// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsTaxonomyF
	{
		/// <summary>
		/// Execute Posts Taxonomy query
		/// </summary>
		/// <typeparam name="TTerm">Term Entity type</typeparam>
		/// <typeparam name="TModel">Return Model type</typeparam>
		/// <param name="query">IWpDbQuery</param>
		/// <param name="opt">Function to return query options</param>
		public static Task<Option<IEnumerable<TModel>>> ExecuteAsync<TTerm, TModel>(
			IWpDbQuery query,
			Query.GetTermOptions<TTerm> opt
		)
			where TTerm : WpTermEntity
			where TModel : IWithId =>
			Return(
				() => opt(new Query.PostsTaxonomy<TTerm>(query.Db)),
				e => new Msg.ErrorGettingQueryPostsTaxonomyOptionsMsg(e)
			)
			.Bind(
				x => x.GetParts<TModel>()
			)
			.BindAsync(
				x => query.QueryAsync<TModel>(x)
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get posts taxonomy query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryPostsTaxonomyOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
