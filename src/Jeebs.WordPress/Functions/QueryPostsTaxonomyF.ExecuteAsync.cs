// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Messages;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Query;
using StrongId;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsTaxonomyF
{
	/// <summary>
	/// Execute Posts Taxonomy query
	/// </summary>
	/// <typeparam name="TModel">Return Model type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="opt">Function to return query options</param>
	public static Task<Maybe<IEnumerable<TModel>>> ExecuteAsync<TModel>(IWpDb db, IUnitOfWork w, GetPostsTaxonomyOptions opt)
		where TModel : IWithId<WpTermId> =>
		F.Some(
			() => opt(new PostsTaxonomyOptions(db.Schema)),
			e => new M.ErrorGettingQueryPostsTaxonomyOptionsMsg(e)
		)
		.Bind(
			x => x.ToParts<TModel>()
		)
		.BindAsync(
			x => db.Query.QueryAsync<TModel>(x, w.Transaction)
		);

	/// <summary>Messages</summary>
	public static partial class M
	{
		/// <summary>Unable to get posts taxonomy query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingQueryPostsTaxonomyOptionsMsg(Exception Value) : ExceptionMsg;
	}
}
