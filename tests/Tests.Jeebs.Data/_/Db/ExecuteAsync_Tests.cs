// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests
{
	public class ExecuteAsync_Tests
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
			_ = await db.ExecuteAsync(query, parameters, type);

			// Assert
			client.Received().Connect(Arg.Any<string>());
			connection.Received().BeginTransaction();
		}

		[Fact]
		public async Task No_Return_Logs_Query_Info_To_Verbose()
		{
			// Arrange
			var (_, log, _, _, db) = Db_Setup.Get();
			var query = F.Rnd.Str;
			var parameters = F.Rnd.Guid.ToString();
			const CommandType type = CommandType.Text;
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			_ = await db.ExecuteAsync(query, parameters, type);
			_ = await db.ExecuteAsync(query, parameters, type, transaction);

			// Assert
			log.Received(2).Verbose("{Type}: {Query} Parameters: {@Parameters}", type, query, parameters);
		}

		[Fact]
		public async Task With_Return_Logs_Query_Info_To_Verbose()
		{
			// Arrange
			var (_, log, _, _, db) = Db_Setup.Get();
			var query = F.Rnd.Str;
			var parameters = F.Rnd.Guid.ToString();
			const CommandType type = CommandType.Text;
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			_ = await db.ExecuteAsync<int>(query, parameters, type);
			_ = await db.ExecuteAsync<int>(query, parameters, type, transaction);

			// Assert
			log.Received(2).Verbose("{Type}: {Query} Parameters: {@Parameters}", type, query, parameters);
		}
	}
}
