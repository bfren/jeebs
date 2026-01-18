// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs.Collections;
using Jeebs.Extensions;
using Jeebs.Messages;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Functions;

public static partial class QueryAttachmentsF
{
	/// <summary>
	/// Build custom query to return file attachments with URL from meta values.
	/// </summary>
	/// <param name="schema">IWpDbSchema</param>
	/// <param name="fileIds">Attachment IDs</param>
	/// <param name="virtualUploadsUrl">Virtual Uploads URL for building URLs</param>
	internal static Maybe<string> GetQuery(IWpDbSchema schema, IImmutableList<WpPostId> fileIds, string virtualUploadsUrl)
	{
		// Check for empty list
		if (fileIds.Count == 0)
		{
			return F.None<string, M.NoFileIdsMsg>();
		}

		// Build query
		return
			"SELECT " +
				$"`p`.`{schema.Posts.Id}` AS '{nameof(PostAttachment.Id)}', " +
				$"`p`.`{schema.Posts.Title}` AS '{nameof(PostAttachment.Title)}', " +
				$"`p`.`{schema.Posts.Excerpt}` AS '{nameof(PostAttachment.Description)}', " +
				$"`p`.`{schema.Posts.Url}` AS '{nameof(PostAttachment.Url)}', " +
				$"`pm`.`{schema.PostsMeta.Value}` AS '{nameof(PostAttachment.UrlPath)}', " +
				$"CONCAT('{virtualUploadsUrl.EndWith('/')}', `pm`.`{schema.PostsMeta.Value}`) AS '{nameof(PostAttachment.Url)}' " +
			$"FROM `{schema.Posts}` AS `p` " +
				$"LEFT JOIN `{schema.PostsMeta}` AS `pm` ON `p`.`{schema.Posts.Id}` = `pm`.`{schema.PostsMeta.PostId}` " +
			$"WHERE `p`.`{schema.Posts.Id}` IN ({string.Join(',', fileIds.Select(x => x.Value))}) " +
				$"AND `pm`.`{schema.PostsMeta.Key}` = '{Constants.Attachment}';"
		;
	}

	public static partial class M
	{
		/// <summary>No File IDs have been passed to <see cref="GetQuery(IWpDbSchema, IImmutableList{WpPostId}, string)"/></summary>
		public sealed record class NoFileIdsMsg : Msg;
	}
}
