// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Query;

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
		await query.QueryAsync<int>(value, param, input);
		await query.QueryAsync<int>(value, param, input, transaction);

		// Assert
		await db.Received().QueryAsync<int>(value, param, input, Arg.Any<IDbTransaction>());
		await db.Received().QueryAsync<int>(value, param, input, transaction);
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
		await query.QueryAsync<int>(value, param);
		await query.QueryAsync<int>(value, param, transaction);

		// Assert
		await db.Received().QueryAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
		await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction);
	}

	[Fact]
	public async Task WithParts_GetQuery_Exception_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
	{
		// Arrange
		var (parts, _) = DbQuery_Setup.GetParts();
		var (_, client, _, query) = DbQuery_Setup.Get();
		var transaction = Substitute.For<IDbTransaction>();
		client.GetQuery(parts).Returns(FailGen.Create());

		// Act
		var r0 = await query.QueryAsync<int>(parts);
		var r1 = await query.QueryAsync<int>(parts, transaction);

		// Assert
		_ = r0.AssertFailure();
		_ = r1.AssertFailure();
	}

	[Fact]
	public async Task WithParts_And_Page_GetCountQuery_Fails_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
	{
		// Arrange
		var (parts, _) = DbQuery_Setup.GetParts();
		var (_, client, _, query) = DbQuery_Setup.Get();
		var transaction = Substitute.For<IDbTransaction>();
		client.GetCountQuery(parts).Returns(FailGen.Create());

		// Act
		var r0 = await query.QueryAsync<int>(Rnd.UInt64, parts);
		var r1 = await query.QueryAsync<int>(Rnd.UInt64, parts, transaction);

		// Assert
		_ = r0.AssertFailure();
		_ = r1.AssertFailure();
	}

	[Fact]
	public async Task WithParts_And_Page_GetQuery_Exception_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
	{
		// Arrange
		var (parts, _) = DbQuery_Setup.GetParts();
		var (_, client, _, query) = DbQuery_Setup.Get();
		var transaction = Substitute.For<IDbTransaction>();
		client.GetQuery(Arg.Any<IQueryParts>()).Returns(FailGen.Create());

		// Act
		var r0 = await query.QueryAsync<int>(Rnd.UInt64, parts);
		var r1 = await query.QueryAsync<int>(Rnd.UInt64, parts, transaction);

		// Assert
		_ = r0.AssertFailure();
		_ = r1.AssertFailure();
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
		await query.QueryAsync<int>(parts);
		await query.QueryAsync<int>(parts, transaction);

		// Assert
		client.Received(2).GetQuery(parts);
		await db.Received().QueryAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
		await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction);
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
		await query.QueryAsync<int>(Rnd.UInt64, parts);
		await query.QueryAsync<int>(Rnd.UInt64, parts, transaction);

		// Assert
		client.Received(2).GetCountQuery(parts);
		await db.Received().ExecuteAsync<ulong>(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
		await db.Received().ExecuteAsync<ulong>(value, param, CommandType.Text, transaction);
		client.Received(2).GetQuery(Arg.Any<IQueryParts>());
		await db.Received().QueryAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
		await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction);
	}
}
