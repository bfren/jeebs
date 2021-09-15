// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using F.WordPressF.DataF;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Querying;

namespace Jeebs.WordPress.Data;

public static partial class Query
{
	/// <inheritdoc cref="IQueryTerms"/>
	public sealed class Terms : IQueryTerms
	{
		/// <inheritdoc/>
		public Task<Option<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, IUnitOfWork w, GetTermsOptions opt)
			where T : IWithId<WpTermId>
		{
			return QueryTermsF.ExecuteAsync<T>(db, w, opt);
		}
	}
}
