// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Messages;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.Functions;

public static partial class QueryAttachmentsF
{
	/// <summary>
	/// Get attached files.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="opt">Function to return query options</param>
	internal static Task<Maybe<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, IUnitOfWork w, GetAttachmentsOptions opt)
		where T : IPostAttachment =>
		F.Some(
			() => opt(new AttachmentsOptions()),
			e => new M.ErrorGettingQueryAttachmentsOptionsMsg(e)
		)
		.Bind(
			x => GetQuery(db.Schema, x.Ids, db.WpConfig.VirtualUploadsUrl)
		)
		.BindAsync(
			x => db.QueryAsync<T>(x, null, System.Data.CommandType.Text, w.Transaction)
		);

	public static partial class M
	{
		/// <summary>Unable to get attachments query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingQueryAttachmentsOptionsMsg(Exception Value) : ExceptionMsg;
	}
}
