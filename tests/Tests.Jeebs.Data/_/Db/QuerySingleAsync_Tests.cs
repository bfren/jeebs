// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests;

public class QuerySingleAsync_Tests
{
	[Fact]
	public async Task Without_Transaction_Connects_To_Client_And_Starts_New_Transaction()
	{
		// Arrange
		var (_, _, client, connection, db) = Db_Setup.Get();
		var query = F.Rnd.Str;
		var parameters = F.Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		_ = await db.QuerySingleAsync<int>(query, parameters, type).ConfigureAwait(false);

		// Assert
		_ = client.Received().Connect(Arg.Any<string>());
		_ = connection.Received().BeginTransaction();
	}

	[Fact]
	public async Task Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (_, log, _, _, db) = Db_Setup.Get();
		var query = F.Rnd.Str;
		var parameters = F.Rnd.Guid.ToString();
		const CommandType type = CommandType.TableDirect;
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		_ = await db.QuerySingleAsync<int>(query, parameters, type).ConfigureAwait(false);
		_ = await db.QuerySingleAsync<int>(query, parameters, type, transaction).ConfigureAwait(false);

		// Assert
		log.Received(2).Vrb("{Type}: {Query} Parameters: {@Parameters}", type, query, parameters);
	}
}
