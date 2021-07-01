// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
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
		Task<Option<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, IUnitOfWork w, Query.GetTermsOptions opt)
			where T : IWithId<WpTermId>;
	}
}
