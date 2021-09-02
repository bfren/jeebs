// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Querying;
using static F.OptionF;

namespace F.WordPressF.DataF;

public static partial class QueryTermsF
{
	/// <summary>
	/// Execute Terms query
	/// </summary>
	/// <typeparam name="TModel">Return Model type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="opt">Function to return query options</param>
	public static Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(IWpDb db, IUnitOfWork w, GetTermsOptions opt)
		where TModel : IWithId<WpTermId> =>
		Some(
			() => opt(new Query.TermsOptions(db.Schema)),
			e => new Msg.ErrorGettingQueryTermsOptionsMsg(e)
		)
		.Bind(
			x => x.ToParts<TModel>()
		)
		.BindAsync(
			x => db.Query.QueryAsync<TModel>(x, w.Transaction)
		);

	/// <summary>Messages</summary>
	public static partial class Msg
	{
		/// <summary>Unable to get terms query</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class ErrorGettingQueryTermsOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
	}
}
