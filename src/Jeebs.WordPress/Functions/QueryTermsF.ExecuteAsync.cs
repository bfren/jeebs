// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Messages;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Querying;
using Maybe;
using Maybe.Functions;

namespace Jeebs.WordPress.Functions;

public static partial class QueryTermsF
{
	/// <summary>
	/// Execute Terms query
	/// </summary>
	/// <typeparam name="TModel">Return Model type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="opt">Function to return query options</param>
	public static Task<Maybe<IEnumerable<TModel>>> ExecuteAsync<TModel>(IWpDb db, IUnitOfWork w, GetTermsOptions opt)
		where TModel : Id.IWithId<WpTermId> =>
		MaybeF.Some(
			() => opt(new Query.TermsOptions(db.Schema)),
			e => new M.ErrorGettingQueryTermsOptionsMsg(e)
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
		/// <summary>Unable to get terms query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingQueryTermsOptionsMsg(Exception Value) : ExceptionMsg;
	}
}
