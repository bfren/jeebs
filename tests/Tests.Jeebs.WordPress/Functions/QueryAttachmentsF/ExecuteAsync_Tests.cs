﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Collections;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Functions;
using static Jeebs.WordPress.Functions.QueryAttachmentsF.M;

namespace Jeebs.WordPress.Functions.QueryAttachmentsF_Tests;

public class ExecuteAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Catches_Opt_Exception_Returns_None_With_ErrorGettingQueryAttachmentsOptionsMsg()
	{
		// Arrange
		var (db, w, _) = Setup();

		// Act
		var result = await QueryAttachmentsF.ExecuteAsync<PostAttachment>(db, w, _ => throw new Exception()).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<ErrorGettingQueryAttachmentsOptionsMsg>(none);
	}

	[Fact]
	public async Task Calls_Db_QueryAsync()
	{
		// Arrange
		var (db, w, v) = Setup();
		var fileIds = ImmutableList.Create<WpPostId>(new(Rnd.Lng), new(Rnd.Lng));

		// Act
		_ = await QueryAttachmentsF.ExecuteAsync<PostAttachment>(db, w, opt => (opt with { Ids = fileIds })).ConfigureAwait(false);

		// Assert
		_ = await db.Received().QueryAsync<PostAttachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, v.Transaction).ConfigureAwait(false);
	}
}
