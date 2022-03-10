// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Messages;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Query;
using Maybe;
using Maybe.Functions;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsMetaF
{
	/// <summary>
	/// Execute Posts Meta query
	/// </summary>
	/// <typeparam name="TModel">Return Model type</typeparam>
	/// <param name="db">IWpDbQuery</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="opt">Function to return query options</param>
	public static Task<Maybe<IEnumerable<TModel>>> ExecuteAsync<TModel>(IWpDb db, IUnitOfWork w, GetPostsMetaOptions opt)
		where TModel : Id.IWithId<WpPostMetaId> =>
		MaybeF.Some(
			() => opt(new Query.PostsMetaOptions(db.Schema)),
			e => new M.ErrorGettingQueryPostsMetaOptionsMsg(e)
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
		/// <summary>Unable to get posts meta query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingQueryPostsMetaOptionsMsg(Exception Value) : ExceptionMsg;
	}
}
