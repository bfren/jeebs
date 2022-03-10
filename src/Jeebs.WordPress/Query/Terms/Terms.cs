// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Functions;
using Jeebs.WordPress.Querying;
using Maybe;

namespace Jeebs.WordPress;

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
