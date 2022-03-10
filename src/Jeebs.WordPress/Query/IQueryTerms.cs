// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Querying;
using Maybe;

namespace Jeebs.WordPress;

/// <summary>
/// Query Terms - to enable testing of static functions
/// </summary>
public interface IQueryTerms
{
	/// <summary>
	/// Execute Terms query
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="opt">Function to return query options</param>
	Task<Maybe<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, IUnitOfWork w, GetTermsOptions opt)
		where T : Id.IWithId<WpTermId>;
}
