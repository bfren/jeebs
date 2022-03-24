// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.WordPress.Entities.StrongIds;
using MaybeF.Extensions;
using static Jeebs.WordPress.Functions.QueryAttachmentsF.M;
using Attachment = Jeebs.WordPress.Functions.QueryAttachmentsF.Attachment;

namespace Jeebs.WordPress.Functions.QueryAttachmentsF_Tests;

public class GetFilePathAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Attachment_Not_Found_Returns_None_With_AttachmentNotFoundMsg()
	{
		// Arrange
		var (db, w, _) = Setup();
		var empty = new List<Attachment>().AsEnumerable().Some();
		db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>()).Returns(empty);
		var fileId = new WpPostId(Rnd.Lng);

		// Act
		var result = await QueryAttachmentsF.GetFilePathAsync(db, w, fileId).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<AttachmentNotFoundMsg>(none);
		Assert.Equal(fileId.Value, msg.FileId);
	}

	[Fact]
	public async Task Multiple_Attachments_Found_Returns_None_With_MultipleAttachmentsFoundMsg()
	{
		// Arrange
		var (db, w, _) = Setup();
		var list = new[] { new Attachment(), new Attachment() }.AsEnumerable().Some();
		db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>()).Returns(list);
		var fileId = new WpPostId(Rnd.Lng);

		// Act
		var result = await QueryAttachmentsF.GetFilePathAsync(db, w, fileId).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<MultipleAttachmentsFoundMsg>(none);
		Assert.Equal(fileId.Value, msg.FileId);
	}

	[Fact]
	public async Task Returns_Attachment_File_Path()
	{
		// Arrange
		var (db, w, v) = Setup();
		var urlPath = Rnd.Str;
		var single = new[] { new Attachment { UrlPath = urlPath } }.AsEnumerable().Some();
		db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>()).Returns(single);

		// Act
		var result = await QueryAttachmentsF.GetFilePathAsync(db, w, new(Rnd.Lng)).ConfigureAwait(false);

		// Assert
		var some = result.AssertSome();
		Assert.Equal($"{v.UploadsPath}/{urlPath}", some);
	}
}
