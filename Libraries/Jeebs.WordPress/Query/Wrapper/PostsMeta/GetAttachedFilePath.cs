// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data.Querying;
using static F.OptionF;
using Msg = Jeebs.WordPress.QueryWrapperMsg;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Get the full path to an attached file
		/// </summary>
		/// <param name="postId">Post ID</param>
		/// <param name="uploadsPath">Full path to wp-uploads directory on server</param>
		public async Task<Option<string>> GetAttachedFilePathAsync(long postId, string uploadsPath)
		{
			return await Return(postId)
				.Map(
					getQuery
				)
				.BindAsync(
					getAttachedFiles<AttachedFileMetaValue>,
					e => new Msg.GetAttachedFilesExceptionMsg(e)
				)
				.UnwrapAsync(
					x => x.Single<AttachedFileMetaValue>(tooMany: () => new Msg.MultipleAttachedFilesFoundMsg(postId))
				)
				.MapAsync(
					addUploadsPath
				);

			// Get query
			IQuery<AttachedFileMetaValue> getQuery(long postId) =>
				GetPostsMetaQuery<AttachedFileMetaValue>(opt =>
				{
					opt.PostId = postId;
					opt.Key = Constants.AttachedFile;
				});

			// Execute query
			async Task<Option<List<T>>> getAttachedFiles<T>(IQuery<T> query) =>
				await query.ExecuteQueryAsync();

			// Add uploads path 
			string addUploadsPath(AttachedFileMetaValue attachedFile) =>
				uploadsPath.EndWith('/') + attachedFile.Value;
		}

		private record AttachedFileMetaValue(string Value);
	}

	namespace QueryWrapperMsg
	{
		/// <summary>An exception occured while getting attached files</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record GetAttachedFilesExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>Multiple attached files found</summary>
		/// <param name="AttachedFileId">Attached File (Post) ID</param>
		public sealed record MultipleAttachedFilesFoundMsg(long AttachedFileId) : IMsg { }
	}
}
