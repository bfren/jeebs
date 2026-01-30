// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.QueryBuilder;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get query parts using the specific options.
	/// </summary>
	/// <typeparam name="TModel">Return value type.</typeparam>
	/// <param name="db">IWpDb.</param>
	/// <param name="opt">Function to return query options.</param>
	internal static Result<IQueryParts> GetQueryParts<TModel>(IWpDb db, GetPostsOptions opt)
		where TModel : IWithId<WpPostId, ulong> =>
		R.Try(
			() => opt(new PostsOptions(db.Schema)),
			e => R.Fail(e).Msg("Error getting posts options.")
				.Ctx(nameof(QueryPostsF), nameof(GetQueryParts))
		)
		.Bind(
			x => x.ToParts<TModel>()
		);
}
