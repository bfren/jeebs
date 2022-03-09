// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs.Collections;
using Jeebs.Extensions;
using Jeebs.Messages;
using Jeebs.WordPress.Entities;
using Maybe;
using Maybe.Functions;

namespace Jeebs.WordPress.Functions;

public static partial class QueryAttachmentsF
{
	/// <summary>
	/// Build custom query to return file attachments with URL from meta values
	/// </summary>
	/// <param name="schema">IWpDbSchema</param>
	/// <param name="fileIds">Attachment IDs</param>
	/// <param name="virtualUploadsUrl">Virtual Uploads URL for building URLs</param>
	internal static Maybe<string> GetQuery(IWpDbSchema schema, IImmutableList<WpPostId> fileIds, string virtualUploadsUrl)
	{
		// Check for empty list
		if (fileIds.Count == 0)
		{
			return MaybeF.None<string, M.NoFileIdsMsg>();
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
			$"FROM `{schema.Post}` AS `p` " +
				$"LEFT JOIN `{schema.PostMeta}` AS `pm` ON `p`.`{schema.Post.Id}` = `pm`.`{schema.PostMeta.PostId}` " +
			$"WHERE `p`.`{schema.Post.Id}` IN ({string.Join(',', fileIds.Select(x => x.Value))}) " +
				$"AND `pm`.`{schema.PostMeta.Key}` = '{Constants.Attachment}';"
		;
	}

	public static partial class M
	{
		/// <summary>No File IDs have been passed to <see cref="GetQuery(IWpDbSchema, IImmutableList{WpPostId}, string)"/></summary>
		public sealed record class NoFileIdsMsg : Msg;
	}
}
