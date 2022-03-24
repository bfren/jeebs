// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Db_Tests;

public class ExecuteAsync_Tests
{
	[Fact]
	public async Task Without_Transaction_Connects_To_Client_And_Starts_New_Transaction()
	{
		// Arrange
		var (_, _, client, connection, db) = Db_Setup.Get();
		var query = Rnd.Str;
		var parameters = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.ExecuteAsync(query, parameters, type).ConfigureAwait(false);

		// Assert
		client.Received().Connect(Arg.Any<string>());
		connection.Received().BeginTransaction();
	}

	[Fact]
	public async Task No_Return_Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (_, log, _, _, db) = Db_Setup.Get();
		var query = Rnd.Str;
		var parameters = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await db.ExecuteAsync(query, parameters, type).ConfigureAwait(false);
		await db.ExecuteAsync(query, parameters, type, transaction).ConfigureAwait(false);

		// Assert
		log.Received(2).Vrb("{Type}: {Query} Parameters: {@Parameters}", type, query, parameters);
	}

	[Fact]
	public async Task With_Return_Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (_, log, _, _, db) = Db_Setup.Get();
		var query = Rnd.Str;
		var parameters = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await db.ExecuteAsync<int>(query, parameters, type).ConfigureAwait(false);
		await db.ExecuteAsync<int>(query, parameters, type, transaction).ConfigureAwait(false);

		// Assert
		log.Received(2).Vrb("{Type}: {Query} Parameters: {@Parameters}", type, query, parameters);
	}
}
