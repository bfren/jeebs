// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data.Common;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.Functions;

public static partial class QueryTermsF
{
	/// <summary>
	/// Execute Terms query.
	/// </summary>
	/// <typeparam name="TModel">Return Model type.</typeparam>
	/// <param name="db">IWpDb.</param>
	/// <param name="w">IUnitOfWork.</param>
	/// <param name="opt">Function to return query options.</param>
	public static Task<Result<IEnumerable<TModel>>> ExecuteAsync<TModel>(IWpDb db, IUnitOfWork w, GetTermsOptions opt)
		where TModel : IWithId<WpTermId, ulong> =>
		R.Try(
			() => opt(new TermsOptions(db.Schema)),
			e => R.Fail(e).Msg("Error getting query terms options.")
				.Ctx(nameof(QueryTermsF), nameof(ExecuteAsync))
		)
		.Bind(
			x => x.ToParts<TModel>()
		)
		.BindAsync(
			x => db.Query.QueryAsync<TModel>(x, w.Transaction)
		);
}
