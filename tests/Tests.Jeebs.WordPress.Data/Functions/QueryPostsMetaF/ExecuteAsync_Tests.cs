// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static F.WordPressF.DataF.QueryPostsMetaF;
using static F.WordPressF.DataF.QueryPostsMetaF.Msg;

namespace F.WordPressF.DataF.QueryPostsMetaF_Tests;

public class ExecuteAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Catches_Opt_Exception_Returns_None_With_ErrorGettingQueryAttachmentsOptionsMsg()
	{
		// Arrange
		var (db, w, _) = Setup();

		// Act
		var result = await ExecuteAsync<Test>(db, w, _ => throw new System.Exception());

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ErrorGettingQueryPostsMetaOptionsMsg>(none);
	}

	[Fact]
	public async Task Calls_Db_QueryAsync()
	{
		// Arrange
		var (db, w, v) = Setup();

		// Act
		var result = await ExecuteAsync<Test>(db, w, opt => opt);

		// Assert
		await db.Query.Received().QueryAsync<Test>(Arg.Any<IQueryParts>(), v.Transaction);
	}

	public record class Test : WpPostMetaEntity;
}
