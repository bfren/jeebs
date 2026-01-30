// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data.Common;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.Functions;

public static partial class QueryAttachmentsF
{
	/// <summary>
	/// Get attached files.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="db">IWpDb.</param>
	/// <param name="w">IUnitOfWork.</param>
	/// <param name="opt">Function to return query options.</param>
	internal static Task<Result<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, IUnitOfWork w, GetAttachmentsOptions opt)
		where T : IPostAttachment =>
		R.Try(
			() => opt(new AttachmentsOptions()),
			e => R.Fail(e).Msg("Error getting query attachments options.")
				.Ctx(nameof(QueryAttachmentsF), nameof(ExecuteAsync))
		)
		.Bind(
			x => GetQuery(db.Schema, x.Ids, db.WpConfig.VirtualUploadsUrl)
		)
		.BindAsync(
			x => db.QueryAsync<T>(x, null, System.Data.CommandType.Text, w.Transaction)
		);
}
