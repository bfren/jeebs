// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Linq;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryAttachmentsF
	{
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
			return
				"SELECT " +
					$"`p`.`{schema.Post.Title}` AS '{nameof(WpAttachmentEntity.Title)}', " +
					$"`p`.`{schema.Post.Excerpt}` AS '{nameof(WpAttachmentEntity.Description)}', " +
					$"`pm`.`{schema.PostMeta.Value}` AS '{nameof(WpAttachmentEntity.UrlPath)}', " +
					$"CONCAT('{virtualUploadsUrl.EndWith('/')}', `pm`.`{schema.PostMeta.Value}`) AS '{nameof(WpAttachmentEntity.Url)}' " +
				$"FROM `{schema.Post}` AS `p` " +
					$"LEFT JOIN `{schema.PostMeta}` AS `pm` ON `p`.`{schema.Post.PostId}` = `pm`.`{schema.PostMeta.PostId}` " +
				$"WHERE `p`.`{schema.Post.PostId}` IN ({string.Join(',', fileIds.Select(x => x.Value))}) " +
					$"AND `pm`.`{schema.PostMeta.Key}` = '{Constants.Attachment}';"
			;
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>No File IDs have been passed to <see cref="GetQuery(IWpDbSchema, IImmutableList{WpPostId}, string)"/></summary>
			public sealed record NoFileIdsMsg : IMsg { }
		}
	}
}
