// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static F.WordPressF.DataF.QueryAttachmentsF;
using static F.WordPressF.DataF.QueryAttachmentsF.M;

namespace F.WordPressF.DataF.QueryAttachmentsF_Tests;

public class ExecuteAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Catches_Opt_Exception_Returns_None_With_ErrorGettingQueryAttachmentsOptionsMsg()
	{
		// Arrange
		var (db, w, _) = Setup();

		// Act
		var result = await ExecuteAsync<PostAttachment>(db, w, _ => throw new System.Exception()).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ErrorGettingQueryAttachmentsOptionsMsg>(none);
	}

	[Fact]
	public async Task Calls_Db_QueryAsync()
	{
		// Arrange
		var (db, w, v) = Setup();
		var fileIds = ImmutableList.Create<WpPostId>(new(Rnd.Lng), new(Rnd.Lng));

		// Act
		var result = await ExecuteAsync<PostAttachment>(db, w, opt => (opt with { Ids = fileIds })).ConfigureAwait(false);

		// Assert
		await db.Received().QueryAsync<PostAttachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, v.Transaction).ConfigureAwait(false);
	}
}
