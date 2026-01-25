// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Functions;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="IQueryTerms"/>
public sealed class Terms : IQueryTerms
{
	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, IUnitOfWork w, GetTermsOptions opt)
		where T : IWithId<WpTermId, ulong> =>
		QueryTermsF.ExecuteAsync<T>(db, w, opt);
}
