// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Functions;
using Jeebs.WordPress.Data.Querying;
using Maybe;

namespace Jeebs.WordPress.Data;

public static partial class Query
{
	/// <inheritdoc cref="IQueryTerms"/>
	public sealed class Terms : IQueryTerms
	{
		/// <inheritdoc/>
		public Task<Maybe<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, IUnitOfWork w, GetTermsOptions opt)
			where T : Id.IWithId<WpTermId> =>
			QueryTermsF.ExecuteAsync<T>(db, w, opt);
	}
}
