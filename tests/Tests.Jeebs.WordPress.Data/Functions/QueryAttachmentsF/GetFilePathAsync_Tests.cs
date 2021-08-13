﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using Jeebs;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static F.WordPressF.DataF.QueryAttachmentsF;
using static F.WordPressF.DataF.QueryAttachmentsF.Msg;

namespace F.WordPressF.DataF.QueryAttachmentsF_Tests
{
	public class GetFilePathAsync_Tests : Query_Tests
	{
		[Fact]
		public async Task Attachment_Not_Found_Returns_None_With_AttachmentNotFoundMsg()
		{
			// Arrange
			var (db, w, _) = Setup();
			var empty = new List<Attachment>().AsEnumerable().Return();
			db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>()).Returns(empty);
			var fileId = new WpPostId(Rnd.Ulng);

			// Act
			var result = await GetFilePathAsync(db, w, fileId);

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
			var list = new[] { new Attachment(), new Attachment() }.AsEnumerable().Return();
			db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>()).Returns(list);
			var fileId = new WpPostId(Rnd.Ulng);

			// Act
			var result = await GetFilePathAsync(db, w, fileId);

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
			var single = new[] { new Attachment { UrlPath = urlPath } }.AsEnumerable().Return();
			db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, Arg.Any<IDbTransaction>()).Returns(single);

			// Act
			var result = await GetFilePathAsync(db, w, new(Rnd.Ulng));

			// Assert
			var some = result.AssertSome();
			Assert.Equal($"{v.UploadsPath}/{urlPath}", some);
		}
	}
}
