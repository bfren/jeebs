// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
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
		/// <param name="virtualUploadsUrl">Virtual Uploads URL for building URLs</param>
		internal static Task<Option<IEnumerable<T>>> ExecuteAsync<T>(IWpDb db, Query.GetAttachmentsOptions opt, string virtualUploadsUrl)
			where T : IAttachment
		{
			return
				Return(
					() => opt(new()),
					e => new Msg.ErrorGettingQueryAttachmentsOptionsMsg(e)
				)
				.Bind(
					x => GetQuery(db.Schema, x.Ids, virtualUploadsUrl)
				)
				.SwitchAsync(
					some: x => db.QueryAsync<T>(x, null, System.Data.CommandType.Text),
					none: () => new List<T>()
				);
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get attachments query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryAttachmentsOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
