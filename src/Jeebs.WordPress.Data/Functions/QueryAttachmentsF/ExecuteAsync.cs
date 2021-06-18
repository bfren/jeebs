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

		/// <summary>
		/// Build custom query to return file attachments with URL from meta values
		/// </summary>
		/// <param name="schema">IWpDbSchema</param>
		/// <param name="fileIds">Attachment IDs</param>
		/// <param name="virtualUploadsUrl">Virtual Uploads URL for building URLs</param> 
		internal static Option<string> GetQuery(IWpDbSchema schema, IImmutableList<WpPostId> fileIds, string virtualUploadsUrl)
		{
			// Check for empty list
			if (fileIds.Count == 0)
			{
				return None<string, Msg.NoFileIdsMsg>();
			}

			// Build query
			return "SELECT " +
				$"`p`.`{schema.Post.Title}` AS '{nameof(WpAttachmentEntity.Title)}', " +
				$"`p`.`{schema.Post.Excerpt}` AS '{nameof(WpAttachmentEntity.Description)}', " +
				$"CONCAT('{virtualUploadsUrl.EndWith('/')}', `pm`.`{schema.PostMeta.Value}`) AS '{nameof(WpAttachmentEntity.Url)}' " +
			$"FROM `{schema.Post}` AS `p` " +
				$"LEFT JOIN `{schema.PostMeta}` AS `pm` ON `p`.`{schema.Post.PostId}` = `pm`.`{schema.PostMeta.PostId}` " +
			$"WHERE `p`.`{schema.Post.PostId}` IN ({string.Join(',', fileIds)}) " +
				$"AND `pm`.`{schema.PostMeta.Key}` = '{Constants.Attachment}';"
			;
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get attachments query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryAttachmentsOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>No File IDs have been passed to <see cref="GetQuery(IWpDbSchema, IImmutableList{WpPostId}, string)"/></summary>
			public sealed record NoFileIdsMsg : IMsg { }
		}
	}
}
