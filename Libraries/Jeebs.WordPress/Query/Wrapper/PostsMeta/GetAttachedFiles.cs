// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.WordPress.Entities.Additional;
using static F.OptionF;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Get attached files
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fileIds">Result - value is list of attached file IDs</param>
		/// <param name="virtualUploadsUrl">Virtual Uploads URL for building URLs</param>
		public Task<Option<List<T>>> GetAttachedFilesAsync<T>(List<long> fileIds, string virtualUploadsUrl)
			where T : WpAttachedFile
		{
			// Check for empty list
			if (fileIds.Count == 0)
			{
				return Return(new List<T>()).AsTask;
			}

			// Run query
			return Return(fileIds)
				.Map(
					getQuery,
					e => new Msg.GetAttachedFilesQueryExceptionMsg(e)
				)
				.BindAsync(
					getAttachedFiles
				)
				.MapAsync(
					x => x.ToList(),
					DefaultHandler
				);

			// Build custom query
			string getQuery(List<long> fileIds) =>
				"SELECT " +
					$"`p`.`{db.Post.Title}` AS '{nameof(WpAttachedFile.Title)}', " +
					$"`p`.`{db.Post.Excerpt}` AS '{nameof(WpAttachedFile.Description)}', " +
					$"CONCAT('{virtualUploadsUrl.EndWith('/')}', `pm`.`{db.PostMeta.Value}`) AS '{nameof(WpAttachedFile.Url)}' " +
				$"FROM `{db.Post}` AS `p` " +
					$"LEFT JOIN `{db.PostMeta}` AS `pm` ON `p`.`{db.Post.PostId}` = `pm`.`{db.PostMeta.PostId}` " +
				$"WHERE `p`.`{db.Post.PostId}` IN ({string.Join(',', fileIds)}) " +
					$"AND `pm`.`{db.PostMeta.Key}` = '{Constants.AttachedFile}';"
				;

			// Get the files
			async Task<Option<IEnumerable<T>>> getAttachedFiles(string query)
			{
				// Start unit of work
				using var w = db.UnitOfWork;

				// Execute query
				return await w.QueryAsync<T>(query);
			}
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get attached files query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetAttachedFilesQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
