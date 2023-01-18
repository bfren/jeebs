// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Db_Tests;

public class ExecuteAsync_Tests
{
	[Fact]
	public async Task Without_Transaction_Starts_UnitOfWork()
	{
		// Arrange
		var (db, _) = Db_Setup.Get();
		var query = Rnd.Str;
		var parameters = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.ExecuteAsync(query, parameters, type);

		// Assert
		await db.Received().StartWorkAsync();
	}

	[Fact]
	public async Task No_Return_Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Db_Setup.Get();
		var query = Rnd.Str;
		var parameters = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await db.ExecuteAsync(query, parameters, type, v.Transaction);

		// Assert
		v.Log.Received().Vrb("Query Type: {Type} | Return: {Return} | {Query} | Parameters: {Parameters}", type, typeof(bool), query, parameters);
	}

	[Fact]
	public async Task With_Return_Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Db_Setup.Get();
		var query = Rnd.Str;
		var parameters = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.ExecuteAsync<int>(query, parameters, type);
		await db.ExecuteAsync<int>(query, parameters, type, v.Transaction);

		// Assert
		v.Log.Received().Vrb("Query Type: {Type} | Return: {Return} | {Query} | Parameters: {Parameters}", type, typeof(int), query, parameters);
	}
}
