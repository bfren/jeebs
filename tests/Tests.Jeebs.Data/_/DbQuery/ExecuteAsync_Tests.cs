// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

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
		var value = F.Rnd.Str;
		var param = F.Rnd.Int;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		_ = await query.ExecuteAsync(value, param, input).ConfigureAwait(false);
		_ = await query.ExecuteAsync(value, param, input, transaction).ConfigureAwait(false);
		_ = await query.ExecuteAsync<long>(value, param, input).ConfigureAwait(false);
		_ = await query.ExecuteAsync<long>(value, param, input, transaction).ConfigureAwait(false);

		// Assert
		_ = await db.Received().ExecuteAsync(value, param, input, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await db.Received().ExecuteAsync(value, param, input, transaction).ConfigureAwait(false);
		_ = await db.Received().ExecuteAsync<long>(value, param, input, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await db.Received().ExecuteAsync<long>(value, param, input, transaction).ConfigureAwait(false);
	}

	[Fact]
	public async Task Calls_Db_ExecuteAsync_As_Text()
	{
		// Arrange
		var (db, _, _, query) = DbQuery_Setup.Get();
		var value = F.Rnd.Str;
		var param = F.Rnd.Int;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		_ = await query.ExecuteAsync(value, param).ConfigureAwait(false);
		_ = await query.ExecuteAsync(value, param, transaction).ConfigureAwait(false);
		_ = await query.ExecuteAsync<long>(value, param).ConfigureAwait(false);
		_ = await query.ExecuteAsync<long>(value, param, transaction).ConfigureAwait(false);

		// Assert
		_ = await db.Received().ExecuteAsync(value, param, CommandType.Text, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await db.Received().ExecuteAsync(value, param, CommandType.Text, transaction).ConfigureAwait(false);
		_ = await db.Received().ExecuteAsync<long>(value, param, CommandType.Text, Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await db.Received().ExecuteAsync<long>(value, param, CommandType.Text, transaction).ConfigureAwait(false);
	}
}
