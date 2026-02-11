// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Common.Db_Tests;

public class InsertAsync_Tests : Db_Setup
{
	[Fact]
	public async Task Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.InsertAsync<int>(query, param, type);
		await db.InsertAsync<int>(query, param, type, v.Transaction);

		// Assert
		v.Log.Received(2).Vrb(
			"Query Type: {Type} | Returns: {Return} | {Query} | Parameters: {Param}",
			type, typeof(int), query, param
		);
	}
}
