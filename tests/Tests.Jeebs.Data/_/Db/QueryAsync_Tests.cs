// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Db_Tests;

public class QueryAsync_Tests
{
	[Fact]
	public async Task Without_Transaction_Starts_UnitOfWork()
	{
		// Arrange
		var (db, v) = Db_Setup.Get();
		var query = Rnd.Str;
		var parameters = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.QueryAsync<int>(query, parameters, type);

		// Assert
		await db.Received().StartWorkAsync();
	}

	[Fact]
	public async Task Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Db_Setup.Get();
		var query = Rnd.Str;
		var parameters = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.QueryAsync<int>(query, parameters, type, v.Transaction);

		// Assert
		v.Log.Received().Vrb("Query Type: {Type} | Return: {Return} | {Query} | Parameters: {Parameters}", type, typeof(int), query, parameters);
	}
}
