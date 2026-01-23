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
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.QueryAsync<int>(query, param, type);

		// Assert
		await db.Received().StartWorkAsync();
	}

	[Fact]
	public async Task Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Db_Setup.Get();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.QueryAsync<int>(query, param, type, v.Transaction);

		// Assert
		v.Log.Received().Vrb(
			"Query Type: {Type} | Return: {Return} | {Query} | Parameters: {Param}",
			Arg.Is<object>(x => Db_Setup.Cmp(new { type, Return = typeof(int), query, param }, x))
		);
	}
}
