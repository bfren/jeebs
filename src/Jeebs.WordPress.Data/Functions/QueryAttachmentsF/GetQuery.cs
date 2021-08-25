// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
					$"`p`.`{schema.Post.Id}` AS '{nameof(PostAttachment.Id)}', " +
					$"`p`.`{schema.Post.Title}` AS '{nameof(PostAttachment.Title)}', " +
					$"`p`.`{schema.Post.Excerpt}` AS '{nameof(PostAttachment.Description)}', " +
					$"`p`.`{schema.Post.Url}` AS '{nameof(PostAttachment.Url)}', " +
					$"`pm`.`{schema.PostMeta.Value}` AS '{nameof(PostAttachment.UrlPath)}', " +
					$"CONCAT('{virtualUploadsUrl.EndWith('/')}', `pm`.`{schema.PostMeta.Value}`) AS '{nameof(PostAttachment.Url)}' " +
				$"FROM `{schema.Post.GetName()}` AS `p` " +
					$"LEFT JOIN `{schema.PostMeta.GetName()}` AS `pm` ON `p`.`{schema.Post.Id}` = `pm`.`{schema.PostMeta.PostId}` " +
				$"WHERE `p`.`{schema.Post.Id}` IN ({string.Join(',', fileIds.Select(x => x.Value))}) " +
					$"AND `pm`.`{schema.PostMeta.Key}` = '{Constants.Attachment}';"
			;
		}

		public static partial class Msg
		{
			/// <summary>No File IDs have been passed to <see cref="GetQuery(IWpDbSchema, IImmutableList{WpPostId}, string)"/></summary>
			public sealed record class NoFileIdsMsg : IMsg { }
		}
	}
}
