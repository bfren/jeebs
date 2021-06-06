// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.WordPress.Data.Querying;
using static F.OptionF;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Get the full path to an attached file
		/// </summary>
		/// <param name="postId">Post ID</param>
		/// <param name="uploadsPath">Full path to wp-uploads directory on server</param>
		public Task<Option<string>> GetAttachedFilePathAsync(long postId, string uploadsPath)
		{
			return Return(postId)
				.Bind(
					getQuery
				)
				.BindAsync(
					getAttachedFiles<AttachedFileMetaValue>
				)
				.UnwrapAsync(
					x => x.Single<AttachedFileMetaValue>(tooMany: () => new Msg.MultipleAttachedFilesFoundMsg(postId))
				)
				.MapAsync(
					addUploadsPath,
					DefaultHandler
				);

			// Get query
			Option<IQuery<AttachedFileMetaValue>> getQuery(long postId) =>
				GetPostsMetaQuery<AttachedFileMetaValue>(opt =>
				{
					opt.PostId = postId;
					opt.Key = Constants.AttachedFile;
				});

			// Execute query
			Task<Option<List<T>>> getAttachedFiles<T>(IQuery<T> query) =>
				query.ExecuteQueryAsync();

			// Add uploads path 
			string addUploadsPath(AttachedFileMetaValue attachedFile) =>
				uploadsPath.EndWith('/') + attachedFile.Value;
		}

		private record AttachedFileMetaValue(string Value);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get attached file path query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetAttachedFilePathQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>An exception occured while getting attached files</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetAttachedFilesExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Multiple attached files found</summary>
			/// <param name="AttachedFileId">Attached File (Post) ID</param>
			public sealed record MultipleAttachedFilesFoundMsg(long AttachedFileId) : IMsg { }
		}
	}
}
