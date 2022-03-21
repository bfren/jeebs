// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get query parts using the specific options
	/// </summary>
	/// <typeparam name="TModel">Return value type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="opt">Function to return query options</param>
	internal static Maybe<IQueryParts> GetQueryParts<TModel>(IWpDb db, GetPostsOptions opt)
		where TModel : Id.IWithId<WpPostId> =>
		F.Some(
			() => opt(new Query.PostsOptions(db.Schema)),
			e => new M.ErrorGettingQueryPostsOptionsMsg(e)
		)
		.Bind(
			x => x.ToParts<TModel>()
		);
}
