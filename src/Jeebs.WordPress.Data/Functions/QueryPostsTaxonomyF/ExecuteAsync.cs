// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Querying;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsTaxonomyF
	{
		/// <summary>
		/// Execute Posts Taxonomy query
		/// </summary>
		/// <typeparam name="TModel">Return Model type</typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="opt">Function to return query options</param>
		public static Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(IWpDb db, IUnitOfWork w, GetPostsTaxonomyOptions opt)
			where TModel : IWithId<WpTermId> =>
			Return(
				() => opt(new Query.PostsTaxonomyOptions(db.Schema)),
				e => new Msg.ErrorGettingQueryPostsTaxonomyOptionsMsg(e)
			)
			.Bind(
				x => x.ToParts<TModel>()
			)
			.BindAsync(
				x => db.Query.QueryAsync<TModel>(x, w.Transaction)
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get posts taxonomy query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record class ErrorGettingQueryPostsTaxonomyOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
