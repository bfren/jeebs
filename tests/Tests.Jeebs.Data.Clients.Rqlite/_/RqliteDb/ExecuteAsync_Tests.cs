// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Logging;
using Rqlite.Client;

namespace Jeebs.Data.Clients.Rqlite.RqliteDb_Tests;

public class ExecuteAsync_Tests
{
	private static RqliteDb Setup(params RqliteCommandResult[] results)
	{
		var rqlite = Substitute.For<IRqliteClient>();
		rqlite.ExecuteAsync(Arg.Any<bool>(), Arg.Any<(string, object)[]>())
			.Returns(R.Wrap(results.ToList()));

		var factory = Substitute.For<IRqliteClientFactory>();
		factory.CreateClientWithDefaults().Returns(rqlite);

		var name = Rnd.Str;
		var config = new DbConfig { Connections = new() { [name] = new() { ConnectionString = string.Empty } } };

		return Substitute.ForPartsOf<RqliteDb>(factory, new RqliteDbClient(), config, Substitute.For<ILog>(), name);
	}

	private static RqliteCommandResult Result(long rowsAffected, long lastInsertId) =>
		new() { RowsAffected = rowsAffected, LastInsertId = lastInsertId };

	[Fact]
	public async Task Rows_Affected__Returns_True()
	{
		// Arrange
		var db = Setup(Result(1, 0));

		// Act
		var result = await db.ExecuteAsync(Rnd.Str, null);

		// Assert
		Assert.True(result.AssertOk());
	}

	[Fact]
	public async Task No_Rows_Affected_With_Previous_Insert_Id__Returns_False()
	{
		// Arrange - e.g. a versioned UPDATE that matched nothing, after an earlier INSERT on the same connection
		var db = Setup(Result(0, Rnd.Lng));

		// Act
		var result = await db.ExecuteAsync(Rnd.Str, null);

		// Assert
		Assert.False(result.AssertOk());
	}

	[Fact]
	public async Task Any_Command_Affects_No_Rows__Returns_False()
	{
		// Arrange
		var db = Setup(Result(1, 0), Result(0, 0));

		// Act
		var result = await db.ExecuteAsync(true, (Rnd.Str, new()), (Rnd.Str, new()));

		// Assert
		Assert.False(result.AssertOk());
	}
}
