// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Functions;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Functions;

public static partial class QueryAttachmentsF
{
	/// <summary>
	/// Get the filesystem path to the specified Attachment.
	/// </summary>
	/// <param name="db">IWpDb.</param>
	/// <param name="w">IUnitOfWork.</param>
	/// <param name="fileId">File (Post) ID.</param>
	internal static Task<Result<string>> GetFilePathAsync(IWpDb db, IUnitOfWork w, WpPostId fileId) =>
		ExecuteAsync<Attachment>(
			db, w, opt => opt with { Ids = ListF.Create(fileId) }
		)
		.GetSingleAsync(
			x => x.Value<Attachment>(),
			(msg, args) => R.Fail("Unable to get attachment for File {Id}: " + msg, [fileId.Value, .. args])
				.Ctx(nameof(QueryAttachmentsF), nameof(GetFilePathAsync))
		)
		.MapAsync(
			x => x.GetFilePath(db.WpConfig.UploadsPath),
			e => R.Fail(e).Msg("Error getting attachment path for File {Id}.", fileId.Value)
				.Ctx(nameof(QueryAttachmentsF), nameof(GetFilePathAsync))
		);

	internal sealed record class Attachment : PostAttachment;
}
