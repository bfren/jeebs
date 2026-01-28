// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.WordPress.Entities.Ids;
using Attachment = Jeebs.WordPress.Functions.QueryAttachmentsF.Attachment;

namespace Jeebs.WordPress.Functions.QueryAttachmentsF_Tests;

public class GetFilePathAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Attachment_Not_Found_Returns_None_With_AttachmentNotFoundMsg()
	{
		// Arrange
		var (db, w, _) = Setup();
		var empty = new List<Attachment>().AsEnumerable().Wrap().AsResult();
		db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>()).Returns(empty);
		var fileId = IdGen.ULongId<WpPostId>();

		// Act
		var result = await QueryAttachmentsF.GetFilePathAsync(db, w, fileId);

		// Assert
		_ = result.AssertFailure("Unable to get attachment for File {Id}: Cannot get single value from an empty list.",
			fileId.Value
		);
	}

	[Fact]
	public async Task Multiple_Attachments_Found_Returns_None_With_MultipleAttachmentsFoundMsg()
	{
		// Arrange
		var (db, w, _) = Setup();
		var list = new[] { new Attachment(), new Attachment() }.AsEnumerable().Wrap().AsResult();
		db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>()).Returns(list);
		var fileId = IdGen.ULongId<WpPostId>();

		// Act
		var result = await QueryAttachmentsF.GetFilePathAsync(db, w, fileId);

		// Assert
		_ = result.AssertFailure("Unable to get attachment for File {Id}: Cannot get single value from a list with multiple values.",
			fileId.Value
		);
	}

	[Fact]
	public async Task Returns_Attachment_File_Path()
	{
		// Arrange
		var (db, w, v) = Setup();
		var urlPath = Rnd.Str;
		var single = new[] { new Attachment { UrlPath = urlPath } }.AsEnumerable().Wrap().AsResult();
		db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>()).Returns(single);

		// Act
		var result = await QueryAttachmentsF.GetFilePathAsync(db, w, IdGen.ULongId<WpPostId>());

		// Assert
		var ok = result.AssertOk();
		Assert.Equal($"{v.UploadsPath}/{urlPath}", ok);
	}
}
