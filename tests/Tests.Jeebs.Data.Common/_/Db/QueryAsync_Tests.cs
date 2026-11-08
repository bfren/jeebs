// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Common.Db_Tests;

public class QueryAsync_Tests : Db_Setup
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
		await db.QueryAsync<int>(query, param, type);

		// Assert
		await v.UnitOfWork.Received(1).DisposeAsync();
	}

	[Fact]
	public async Task With_UnitOfWork__Before_A_Write__Runs_Without_Transaction()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;

		// Act
		await db.QueryAsync<int>(query, null, CommandType.Text, v.UnitOfWork);

		// Assert
		Assert.False(v.UnitOfWork.Transaction.Initialised);
		await v.Client.Adapter.Received(1).QueryAsync<int>(v.Connection, null, query, Arg.Any<object?>(), CommandType.Text);
	}

	[Fact]
	public async Task With_UnitOfWork__After_A_Write__Joins_Transaction()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;

		// Act
		await db.ExecuteAsync(Rnd.Str, null, CommandType.Text, v.UnitOfWork);
		await db.QueryAsync<int>(query, null, CommandType.Text, v.UnitOfWork);

		// Assert
		await v.Client.Adapter.Received(1).QueryAsync<int>(v.Connection, v.Transaction, query, Arg.Any<object?>(), CommandType.Text);
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
		await db.QueryAsync<int>(query, param);
		await db.QueryAsync<int>(query, param, type, v.UnitOfWork);

		// Assert
		v.Log.Received(2).Vrb(
			"Query Type: {Type} | Returns: {Return} | {Query} | Parameters: {Param}",
			type, typeof(int), query, param
		);
	}
}
