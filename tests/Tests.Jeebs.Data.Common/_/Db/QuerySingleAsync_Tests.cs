// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Common.Db_Tests;

public class QuerySingleAsync_Tests : Db_Setup
{
	[Fact]
	public async Task Without_UnitOfWork__Starts_And_Disposes_One()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.QuerySingleAsync<int>(query, param, type);

		// Assert
		await v.UnitOfWork.Received(1).DisposeAsync();
	}

	[Fact]
	public async Task Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.QuerySingleAsync<int>(query, param);
		await db.QuerySingleAsync<int>(query, param, type, v.UnitOfWork);

		// Assert
		v.Log.Received(2).Vrb(
			"Query Type: {Type} | Returns: {Return} | {Query} | Parameters: {Param}",
			type, typeof(int), query, param
		);
	}
}
