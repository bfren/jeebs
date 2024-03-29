// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Collections;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.StrongIds;
using static Jeebs.WordPress.Functions.QueryAttachmentsF.M;
using static StrongId.Testing.Generator;

namespace Jeebs.WordPress.Functions.QueryAttachmentsF_Tests;

public class ExecuteAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Catches_Opt_Exception_Returns_None_With_ErrorGettingQueryAttachmentsOptionsMsg()
	{
		// Arrange
		var (db, w, _) = Setup();

		// Act
		var result = await QueryAttachmentsF.ExecuteAsync<PostAttachment>(db, w, _ => throw new Exception());

		// Assert
		result.AssertNone().AssertType<ErrorGettingQueryAttachmentsOptionsMsg>();
	}

	[Fact]
	public async Task Calls_Db_QueryAsync()
	{
		// Arrange
		var (db, w, v) = Setup();
		var fileIds = ImmutableList.Create(ULongId<WpPostId>(), ULongId<WpPostId>());

		// Act
		await QueryAttachmentsF.ExecuteAsync<PostAttachment>(db, w, opt => (opt with { Ids = fileIds }));

		// Assert
		await db.Received().QueryAsync<PostAttachment>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, v.Transaction);
	}
}
