// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Config;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static F.WordPressF.DataF.QueryAttachmentsF;
using static F.WordPressF.DataF.QueryAttachmentsF.Msg;
using static Jeebs.WordPress.Data.Query;

namespace F.WordPressF.DataF.QueryAttachmentsF_Tests
{
	public class GetFilePathAsync_Tests
	{
		private static (IWpDb, Vars) Setup()
		{
			var db = Substitute.For<IWpDb>();

			var schema = new WpDbSchema(Rnd.Str);
			db.Schema.Returns(schema);

			var uploadsPath = Rnd.Str;

			var config = new WpConfig { UploadsPath = uploadsPath };
			db.WpConfig.Returns(config);

			return (db, new(uploadsPath));
		}

		private record Vars(
			string UploadsPath
		);

		[Fact]
		public async Task Attachment_Not_Found_Returns_None_With_AttachmentNotFoundMsg()
		{
			// Arrange
			var (db, _) = Setup();
			var empty = new List<Attachment>().AsEnumerable().Return();
			db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text).Returns(empty);
			var fileId = new WpPostId(Rnd.Lng);

			// Act
			var result = await GetFilePathAsync(db, fileId);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<AttachmentNotFoundMsg>(none);
			Assert.Equal(fileId.Value, msg.FileId);
		}

		[Fact]
		public async Task Multiple_Attachments_Found_Returns_None_With_MultipleAttachmentsFoundMsg()
		{
			// Arrange
			var (db, _) = Setup();
			var list = new[] { new Attachment(), new Attachment() }.AsEnumerable().Return();
			db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text).Returns(list);
			var fileId = new WpPostId(Rnd.Lng);

			// Act
			var result = await GetFilePathAsync(db, fileId);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<MultipleAttachmentsFoundMsg>(none);
			Assert.Equal(fileId.Value, msg.FileId);
		}

		[Fact]
		public async Task Returns_Attachment_File_Path()
		{
			// Arrange
			var (db, v) = Setup();
			var urlPath = Rnd.Str;
			var single = new[] { new Attachment { UrlPath = urlPath } }.AsEnumerable().Return();
			db.QueryAsync<Attachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text).Returns(single);

			// Act
			var result = await GetFilePathAsync(db, new(Rnd.Lng));

			// Assert
			var some = result.AssertSome();
			Assert.Equal($"{v.UploadsPath}/{urlPath}", some);
		}
	}
}
