// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Query;
using NSubstitute.ExceptionExtensions;
using static Jeebs.Data.DbQuery.M;

namespace Jeebs.Data.DbQuery_Tests;

public class QueryAsync_Tests
{
	[Theory]
	[InlineData(CommandType.StoredProcedure)]
	[InlineData(CommandType.TableDirect)]
	[InlineData(CommandType.Text)]
	public async Task Calls_Db_QueryAsync_With_CommandType(CommandType input)
	{
		// Arrange
		var (db, _, _, query) = DbQuery_Setup.Get();
		var value = Rnd.Str;
		var param = Rnd.Int;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		_ = await query.QueryAsync<int>(value, param, input).ConfigureAwait(false);
		_ = await query.QueryAsync<int>(value, param, input, transaction).ConfigureAwait(false);

		// Assert
		_ = await db.Received().QueryAsync<int>(value, param, input, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await db.Received().QueryAsync<int>(value, param, input, transaction).ConfigureAwait(false);
	}

	[Fact]
	public async Task Calls_Db_QueryAsync_As_Text()
	{
		// Arrange
		var (db, _, _, query) = DbQuery_Setup.Get();
		var value = Rnd.Str;
		var param = Rnd.Int;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		_ = await query.QueryAsync<int>(value, param).ConfigureAwait(false);
		_ = await query.QueryAsync<int>(value, param, transaction).ConfigureAwait(false);

		// Assert
		_ = await db.Received().QueryAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction).ConfigureAwait(false);
	}

	[Fact]
	public async Task WithParts_GetQuery_Exception_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
	{
		// Arrange
		var (parts, _) = DbQuery_Setup.GetParts();
		var (_, client, _, query) = DbQuery_Setup.Get();
		var transaction = Substitute.For<IDbTransaction>();
		_ = client.GetQuery(parts).Throws<Exception>();

		// Act
		var r0 = await query.QueryAsync<int>(parts).ConfigureAwait(false);
		var r1 = await query.QueryAsync<int>(parts, transaction).ConfigureAwait(false);

		// Assert
		var n0 = r0.AssertNone();
		_ = Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(n0);
		var n1 = r1.AssertNone();
		_ = Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(n1);
	}

	[Fact]
	public async Task WithParts_And_Page_GetCountQuery_Exception_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
	{
		// Arrange
		var (parts, _) = DbQuery_Setup.GetParts();
		var (_, client, _, query) = DbQuery_Setup.Get();
		var transaction = Substitute.For<IDbTransaction>();
		_ = client.GetCountQuery(parts).Throws<Exception>();

		// Act
		var r0 = await query.QueryAsync<int>(Rnd.Ulng, parts).ConfigureAwait(false);
		var r1 = await query.QueryAsync<int>(Rnd.Ulng, parts, transaction).ConfigureAwait(false);

		// Assert
		var n0 = r0.AssertNone();
		_ = Assert.IsType<ErrorGettingCountQueryFromPartsExceptionMsg>(n0);
		var n1 = r1.AssertNone();
		_ = Assert.IsType<ErrorGettingCountQueryFromPartsExceptionMsg>(n1);
	}

	[Fact]
	public async Task WithParts_And_Page_GetQuery_Exception_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
	{
		// Arrange
		var (parts, _) = DbQuery_Setup.GetParts();
		var (_, client, _, query) = DbQuery_Setup.Get();
		var transaction = Substitute.For<IDbTransaction>();
		_ = client.GetQuery(Arg.Any<IQueryParts>()).ThrowsForAnyArgs<Exception>();

		// Act
		var r0 = await query.QueryAsync<int>(Rnd.Ulng, parts).ConfigureAwait(false);
		var r1 = await query.QueryAsync<int>(Rnd.Ulng, parts, transaction).ConfigureAwait(false);

		// Assert
		var n0 = r0.AssertNone();
		_ = Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(n0);
		var n1 = r1.AssertNone();
		_ = Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(n1);
	}

	[Fact]
	public async Task With_Parts_Calls_Client_GetQuery_And_Db_QueryAsync()
	{
		// Arrange
		var value = Rnd.Str;
		var (parts, param) = DbQuery_Setup.GetParts();
		var (db, client, _, query) = DbQuery_Setup.Get(value, param);
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		_ = await query.QueryAsync<int>(parts).ConfigureAwait(false);
		_ = await query.QueryAsync<int>(parts, transaction).ConfigureAwait(false);

		// Assert
		_ = client.Received(2).GetQuery(parts);
		_ = await db.Received().QueryAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction).ConfigureAwait(false);
	}

	[Fact]
	public async Task With_Parts_And_Page_Calls_Client_GetCountQuery_And_Client_GetQuery_And_Db_QueryAsync()
	{
		// Arrange
		var value = Rnd.Str;
		var (parts, param) = DbQuery_Setup.GetParts();
		var (db, client, _, query) = DbQuery_Setup.Get(value, param);
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		_ = await query.QueryAsync<int>(Rnd.Ulng, parts).ConfigureAwait(false);
		_ = await query.QueryAsync<int>(Rnd.Ulng, parts, transaction).ConfigureAwait(false);

		// Assert
		_ = client.Received(2).GetCountQuery(parts);
		_ = await db.Received().ExecuteAsync<ulong>(value, param, CommandType.Text, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await db.Received().ExecuteAsync<ulong>(value, param, CommandType.Text, transaction).ConfigureAwait(false);
		_ = client.Received(2).GetQuery(Arg.Any<IQueryParts>());
		_ = await db.Received().QueryAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction).ConfigureAwait(false);
	}
}
