// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryAttachmentsF
	{
		/// <summary>
		/// Get attached files
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="opt">Function to return query options</param>
		internal static Task<Option<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, Query.GetAttachmentsOptions opt)
			where T : IAttachment
		{
			return
				Return(
					() => opt(new()),
					e => new Msg.ErrorGettingQueryAttachmentsOptionsMsg(e)
				)
				.Bind(
					x => GetQuery(db.Schema, x.Ids, db.WpConfig.VirtualUploadsUrl)
				)
				.BindAsync(
					x => db.QueryAsync<T>(x, null, System.Data.CommandType.Text)
				);
		}

		public static partial class Msg
		{
			/// <summary>Unable to get attachments query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryAttachmentsOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
