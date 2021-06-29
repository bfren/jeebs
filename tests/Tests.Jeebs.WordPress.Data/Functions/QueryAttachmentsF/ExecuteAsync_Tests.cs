// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Config;
using Jeebs.Data;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static F.WordPressF.DataF.QueryAttachmentsF;
using static F.WordPressF.DataF.QueryAttachmentsF.Msg;

namespace F.WordPressF.DataF.QueryAttachmentsF_Tests
{
	public class ExecuteAsync_Tests
	{
		[Fact]
		public async Task Catches_Opt_Exception_Returns_None_With_ErrorGettingQueryAttachmentsOptionsMsg()
		{
			// Arrange
			var db = Substitute.For<IWpDb>();
			var unitOfWork = Substitute.For<IUnitOfWork>();

			// Act
			var result = await ExecuteAsync<WpAttachmentEntity>(db, unitOfWork, _ => throw new System.Exception());

			// Assert
			var none = result.AssertNone();
			Assert.IsType<ErrorGettingQueryAttachmentsOptionsMsg>(none);
		}

		[Fact]
		public async Task Calls_Db_QueryAsync()
		{
			// Arrange
			var db = Substitute.For<IWpDb>();

			var schema = new WpDbSchema(Rnd.Str);
			db.Schema.Returns(schema);

			var config = new WpConfig();
			db.WpConfig.Returns(config);

			var unitOfWork = Substitute.For<IUnitOfWork>();
			var transaction = Substitute.For<IDbTransaction>();
			unitOfWork.Transaction.Returns(transaction);

			var fileIds = ImmutableList.Create<WpPostId>(new(Rnd.Lng), new(Rnd.Lng));

			// Act
			var result = await ExecuteAsync<WpAttachmentEntity>(db, unitOfWork, opt => opt with { Ids = fileIds });

			// Assert
			await db.Received().QueryAsync<WpAttachmentEntity>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, transaction);
		}
	}
}
