// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Querying;
using static F.MaybeF;

namespace F.WordPressF.DataF;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get query parts using the specific options
	/// </summary>
	/// <typeparam name="TModel">Return value type</typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="opt">Function to return query options</param>
	internal static Maybe<IQueryParts> GetQueryParts<TModel>(IWpDb db, GetPostsOptions opt)
		where TModel : IWithId<WpPostId> =>
		Some(
			() => opt(new Query.PostsOptions(db.Schema)),
			e => new M.ErrorGettingQueryPostsOptionsMsg(e)
		)
		.Bind(
			x => x.ToParts<TModel>()
		);
}
