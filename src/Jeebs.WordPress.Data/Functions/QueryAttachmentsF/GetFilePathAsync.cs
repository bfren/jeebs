// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;

namespace F.WordPressF.DataF;

public static partial class QueryAttachmentsF
{
	/// <summary>
	/// Get the filesystem path to the specified Attachment
	/// </summary>
	/// <param name="db">IWpDb</param>
	/// <param name="w">IUnitOfWork</param>
	/// <param name="fileId">File (Post) ID</param>
	internal static Task<Option<string>> GetFilePathAsync(IWpDb db, IUnitOfWork w, WpPostId fileId)
	{
		return
			ExecuteAsync<Attachment>(
				db, w, opt => opt with { Ids = ImmutableList.Create(fileId) }
			)
			.UnwrapAsync(
				x => x.Single<Attachment>(
					noItems: () => new M.AttachmentNotFoundMsg(fileId.Value),
					tooMany: () => new M.MultipleAttachmentsFoundMsg(fileId.Value)
				)
			)
			.MapAsync(
				x => x.GetFilePath(db.WpConfig.UploadsPath),
				e => new M.ErrorGettingAttachmentFilePathMsg(e, fileId.Value)
			);
	}

	internal record class Attachment : PostAttachment;

	public static partial class M
	{
		/// <summary>Attachment not found</summary>
		/// <param name="FileId">File (Post) ID</param>
		public sealed record class AttachmentNotFoundMsg(long FileId) : Msg;

		/// <summary>Multiple Attachments found</summary>
		/// <param name="FileId">File (Post) ID</param>
		public sealed record class MultipleAttachmentsFoundMsg(long FileId) : Msg;

		/// <summary>Unable to get Attachment file path</summary>
		/// <param name="Value">Exception object</param>
		/// <param name="FileId">File (Post) ID</param>
		public sealed record class ErrorGettingAttachmentFilePathMsg(Exception Value, long FileId) : ExceptionMsg;
	}
}
