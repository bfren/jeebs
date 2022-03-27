// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using Jeebs.WordPress.Entities;
using static Jeebs.WordPress.Functions.QueryTermsF.M;

namespace Jeebs.WordPress.Functions.QueryTermsF_Tests;

public class ExecuteAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Catches_Opt_Exception_Returns_None_With_ErrorGettingQueryAttachmentsOptionsMsg()
	{
		// Arrange
		var (db, w, _) = Setup();

		// Act
		var result = await QueryTermsF.ExecuteAsync<Test>(db, w, _ => throw new Exception()).ConfigureAwait(false);

		// Assert
		result.AssertNone().AssertType<ErrorGettingQueryTermsOptionsMsg>();
	}

	[Fact]
	public async Task Calls_Db_QueryAsync()
	{
		// Arrange
		var (db, w, v) = Setup();

		// Act
		await QueryTermsF.ExecuteAsync<Test>(db, w, opt => opt).ConfigureAwait(false);

		// Assert
		await db.Query.Received().QueryAsync<Test>(Arg.Any<IQueryParts>(), v.Transaction).ConfigureAwait(false);
	}

	public record class Test : WpTermEntity;
}
