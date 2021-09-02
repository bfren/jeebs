// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Querying;
using static F.OptionF;

namespace F.WordPressF.DataF;

public static partial class QueryAttachmentsF
{
	/// <summary>
	/// Get attached files
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="opt">Function to return query options</param>
	internal static Task<Option<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, IUnitOfWork w, GetAttachmentsOptions opt)
		where T : IPostAttachment
	{
		return
			Some(
				() => opt(new Query.AttachmentsOptions()),
				e => new Msg.ErrorGettingQueryAttachmentsOptionsMsg(e)
			)
			.Bind(
				x => GetQuery(db.Schema, x.Ids, db.WpConfig.VirtualUploadsUrl)
			)
			.BindAsync(
				x => db.QueryAsync<T>(x, null, System.Data.CommandType.Text, w.Transaction)
			);
	}

	public static partial class Msg
	{
		/// <summary>Unable to get attachments query</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class ErrorGettingQueryAttachmentsOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
	}
}
