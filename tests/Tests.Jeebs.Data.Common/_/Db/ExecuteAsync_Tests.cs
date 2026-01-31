// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Common.Db_Tests;

public class ExecuteAsync_Tests : Db_Setup
{
	[Fact]
	public async Task Without_Transaction_Starts_UnitOfWork()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.ExecuteAsync(query, param, type);

		// Assert
		await db.Received().StartWorkAsync();
	}

	[Fact]
	public async Task No_Return_Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.ExecuteAsync(query, param);
		await db.ExecuteAsync(query, param, type, v.Transaction);

		// Assert
		v.Log.Received(2).Vrb(
			"Query Type: {Type} | Returns: {Return} | {Query} | Parameters: {Param}",
			type, typeof(bool), query, param
		);
	}

	[Fact]
	public async Task With_Return_Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.ExecuteAsync<int>(query, param, type);
		await db.ExecuteAsync<int>(query, param, type, v.Transaction);

		// Assert
		v.Log.Received(2).Vrb(
			"Query Type: {Type} | Returns: {Return} | {Query} | Parameters: {Param}",
			type, typeof(int), query, param
		);
	}
}
