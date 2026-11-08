// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Common;
using Jeebs.WordPress.Entities.Ids;
using Attachment = Jeebs.WordPress.Functions.QueryAttachmentsF.Attachment;

namespace Jeebs.WordPress.Functions.QueryAttachmentsF_Tests;

public class GetFilePathAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Attachment_Not_Found_Returns_None_With_AttachmentNotFoundMsg()
	{
		// Arrange
		var (db, unitOfWork, _) = Setup();
		var empty = R.Wrap(new List<Attachment>().AsEnumerable());
		db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IUnitOfWork>()).Returns(empty);
		var fileId = IdGen.ULongId<WpPostId>();

		// Act
		var result = await QueryAttachmentsF.GetFilePathAsync(db, unitOfWork, fileId);

		// Assert
		_ = result.AssertFailure("Unable to get attachment for File {Id}: Cannot get single value from an empty list.",
			fileId.Value
		);
	}

	[Fact]
	public async Task Multiple_Attachments_Found_Returns_None_With_MultipleAttachmentsFoundMsg()
	{
		// Arrange
		var (db, unitOfWork, _) = Setup();
		var list = R.Wrap(new[] { new Attachment(), new Attachment() }.AsEnumerable());
		db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IUnitOfWork>()).Returns(list);
		var fileId = IdGen.ULongId<WpPostId>();

		// Act
		var result = await QueryAttachmentsF.GetFilePathAsync(db, unitOfWork, fileId);

		// Assert
		_ = result.AssertFailure("Unable to get attachment for File {Id}: Cannot get single value from a list with multiple values.",
			fileId.Value
		);
	}

	[Fact]
	public async Task Returns_Attachment_File_Path()
	{
		// Arrange
		var (db, unitOfWork, v) = Setup();
		var urlPath = Rnd.Str;
		var single = R.Wrap(new[] { new Attachment { UrlPath = urlPath } }.AsEnumerable());
		db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IUnitOfWork>()).Returns(single);

		// Act
		var result = await QueryAttachmentsF.GetFilePathAsync(db, unitOfWork, IdGen.ULongId<WpPostId>());

		// Assert
		var ok = result.AssertOk();
		Assert.Equal($"{v.UploadsPath}/{urlPath}", ok);
	}
}
