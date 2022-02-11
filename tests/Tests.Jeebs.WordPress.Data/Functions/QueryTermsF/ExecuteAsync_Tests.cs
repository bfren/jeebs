// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static F.WordPressF.DataF.QueryTermsF;
using static F.WordPressF.DataF.QueryTermsF.M;

namespace F.WordPressF.DataF.QueryTermsF_Tests;

public class ExecuteAsync_Tests : Query_Tests
{
	[Fact]
	public async Task Catches_Opt_Exception_Returns_None_With_ErrorGettingQueryAttachmentsOptionsMsg()
	{
		// Arrange
		var (db, w, _) = Setup();

		// Act
		var result = await ExecuteAsync<Test>(db, w, _ => throw new System.Exception()).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ErrorGettingQueryTermsOptionsMsg>(none);
	}

	[Fact]
	public async Task Calls_Db_QueryAsync()
	{
		// Arrange
		var (db, w, v) = Setup();

		// Act
		_ = await ExecuteAsync<Test>(db, w, opt => opt).ConfigureAwait(false);

		// Assert
		await db.Query.Received().QueryAsync<Test>(Arg.Any<IQueryParts>(), v.Transaction).ConfigureAwait(false);
	}

	public record class Test : WpTermEntity;
}
