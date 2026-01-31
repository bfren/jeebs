// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Functions.QueryTermsF_Tests;

public class ExecuteAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Catches_Opt_Exception_Returns_None_With_ErrorGettingQueryAttachmentsOptionsMsg()
	{
		// Arrange
		var (db, w, _) = Setup();

		// Act
		var result = await QueryTermsF.ExecuteAsync<Test>(db, w, _ => throw new Exception());

		// Assert
		_ = result.AssertFailure("Error getting query terms options.");
	}

	[Fact]
	public async Task Calls_Db_QueryAsync()
	{
		// Arrange
		var (db, w, v) = Setup();

		// Act
		await QueryTermsF.ExecuteAsync<Test>(db, w, opt => opt);

		// Assert
		await db.Received().QueryAsync<Test>(Arg.Any<IQueryParts>(), v.Transaction);
	}

	public record class Test : WpTermEntity;
}
