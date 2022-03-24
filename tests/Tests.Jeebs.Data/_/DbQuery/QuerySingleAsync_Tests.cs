﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.DbQuery_Tests;

public class QuerySingleAsync_Tests
{
	[Theory]
	[InlineData(CommandType.StoredProcedure)]
	[InlineData(CommandType.TableDirect)]
	[InlineData(CommandType.Text)]
	public async Task Calls_Db_QuerySingleAsync_With_CommandType(CommandType input)
	{
		// Arrange
		var (db, _, _, query) = DbQuery_Setup.Get();
		var value = Rnd.Str;
		var param = Rnd.Int;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await query.QuerySingleAsync<int>(value, param, input).ConfigureAwait(false);
		await query.QuerySingleAsync<int>(value, param, input, transaction).ConfigureAwait(false);

		// Assert
		await db.Received().QuerySingleAsync<int>(value, param, input, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		await db.Received().QuerySingleAsync<int>(value, param, input, transaction).ConfigureAwait(false);
	}

	[Fact]
	public async Task Calls_Db_QuerySingleAsync_As_Text()
	{
		// Arrange
		var (db, _, _, query) = DbQuery_Setup.Get();
		var value = Rnd.Str;
		var param = Rnd.Int;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await query.QuerySingleAsync<int>(value, param).ConfigureAwait(false);
		await query.QuerySingleAsync<int>(value, param, transaction).ConfigureAwait(false);

		// Assert
		await db.Received().QuerySingleAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		await db.Received().QuerySingleAsync<int>(value, param, CommandType.Text, transaction).ConfigureAwait(false);
	}

	[Fact]
	public async Task With_Parts_Calls_Client_GetQuery_And_Db_QuerySingleAsync()
	{
		// Arrange
		var value = Rnd.Str;
		var (parts, param) = DbQuery_Setup.GetParts();
		var (db, client, _, query) = DbQuery_Setup.Get(value, param);
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await query.QuerySingleAsync<int>(parts).ConfigureAwait(false);
		await query.QuerySingleAsync<int>(parts, transaction).ConfigureAwait(false);

		// Assert
		client.Received(2).GetQuery(parts);
		await db.Received().QuerySingleAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		await db.Received().QuerySingleAsync<int>(value, param, CommandType.Text, transaction).ConfigureAwait(false);
	}
}
