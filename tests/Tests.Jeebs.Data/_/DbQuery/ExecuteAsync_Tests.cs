// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.DbQuery_Tests;

public class ExecuteAsync_Tests
{
	[Theory]
	[InlineData(CommandType.StoredProcedure)]
	[InlineData(CommandType.TableDirect)]
	[InlineData(CommandType.Text)]
	public async Task Calls_Db_ExecuteAsync_With_CommandType(CommandType input)
	{
		// Arrange
		var (db, _, _, query) = DbQuery_Setup.Get();
		var value = Rnd.Str;
		var param = Rnd.Int;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await query.ExecuteAsync(value, param, input);
		await query.ExecuteAsync(value, param, input, transaction);
		await query.ExecuteAsync<long>(value, param, input);
		await query.ExecuteAsync<long>(value, param, input, transaction);

		// Assert
		await db.Received().ExecuteAsync(value, param, input, Arg.Any<IDbTransaction>());
		await db.Received().ExecuteAsync(value, param, input, transaction);
		await db.Received().ExecuteAsync<long>(value, param, input, Arg.Any<IDbTransaction>());
		await db.Received().ExecuteAsync<long>(value, param, input, transaction);
	}

	[Fact]
	public async Task Calls_Db_ExecuteAsync_As_Text()
	{
		// Arrange
		var (db, _, _, query) = DbQuery_Setup.Get();
		var value = Rnd.Str;
		var param = Rnd.Int;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await query.ExecuteAsync(value, param);
		await query.ExecuteAsync(value, param, transaction);
		await query.ExecuteAsync<long>(value, param);
		await query.ExecuteAsync<long>(value, param, transaction);

		// Assert
		await db.Received().ExecuteAsync(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
		await db.Received().ExecuteAsync(value, param, CommandType.Text, transaction);
		await db.Received().ExecuteAsync<long>(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
		await db.Received().ExecuteAsync<long>(value, param, CommandType.Text, transaction);
	}
}
