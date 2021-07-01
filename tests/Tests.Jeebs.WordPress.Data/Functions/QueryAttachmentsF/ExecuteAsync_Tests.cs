// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static F.WordPressF.DataF.QueryAttachmentsF;
using static F.WordPressF.DataF.QueryAttachmentsF.Msg;

namespace F.WordPressF.DataF.QueryAttachmentsF_Tests
{
	public class ExecuteAsync_Tests : Query_Tests
	{
		[Fact]
		public async Task Catches_Opt_Exception_Returns_None_With_ErrorGettingQueryAttachmentsOptionsMsg()
		{
			// Arrange
			var (db, w, _) = Setup();

			// Act
			var result = await ExecuteAsync<WpAttachmentEntity>(db, w, _ => throw new System.Exception());

			// Assert
			var none = result.AssertNone();
			Assert.IsType<ErrorGettingQueryAttachmentsOptionsMsg>(none);
		}

		[Fact]
		public async Task Calls_Db_QueryAsync()
		{
			// Arrange
			var (db, w, v) = Setup();
			var fileIds = ImmutableList.Create<WpPostId>(new(Rnd.Ulng), new(Rnd.Ulng));

			// Act
			var result = await ExecuteAsync<WpAttachmentEntity>(db, w, opt => opt with { Ids = fileIds });

			// Assert
			await db.Received().QueryAsync<WpAttachmentEntity>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, v.Transaction);
		}
	}
}
