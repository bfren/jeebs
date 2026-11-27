// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data.Common;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsMetaF
{
	/// <summary>
	/// Execute Posts Meta query.
	/// </summary>
	/// <typeparam name="TModel">Return Model type.</typeparam>
	/// <param name="db">IWpDbQuery.</param>
	/// <param name="unitOfWork">IUnitOfWork.</param>
	/// <param name="opt">Function to return query options.</param>
	public static Task<Result<IEnumerable<TModel>>> ExecuteAsync<TModel>(IWpDb db, IUnitOfWork unitOfWork, GetPostsMetaOptions opt)
		where TModel : IWithId<WpPostMetaId, ulong> =>
		R.Try(
			() => opt(new PostsMetaOptions(db.Schema)),
			e => ErrorGettingPostsMetaOptions() with { Exception = e }
		)
		.Bind(
			x => x.ToParts<TModel>()
		)
		.BindAsync(
			x => db.QueryAsync<TModel>(x, unitOfWork)
		);

	[Failure("Error getting query posts meta options.", Wrap.Logging.LogLevel.Error)]
	private static partial Failure ErrorGettingPostsMetaOptions();
}
