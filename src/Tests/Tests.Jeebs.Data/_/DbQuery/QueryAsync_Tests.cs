// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbQuery_Tests
{
	public class QueryAsync_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.TableDirect)]
		[InlineData(CommandType.Text)]
		public async Task Calls_Db_QueryAsync_With_CommandType(CommandType input)
		{
			// Arrange
			var (db, _, _, query) = DbQuery_Setup.Get();
			var value = F.Rnd.Str;
			var param = F.Rnd.Int;
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			await query.QueryAsync<int>(value, param, input, transaction);

			// Assert
			await db.Received().QueryAsync<int>(value, param, input, transaction);
		}

		[Fact]
		public async Task Calls_Db_QueryAsync_As_Text()
		{
			// Arrange
			var (db, _, _, query) = DbQuery_Setup.Get();
			var value = F.Rnd.Str;
			var param = F.Rnd.Int;
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			await query.QueryAsync<int>(value, param, transaction);

			// Assert
			await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction);
		}

		[Fact]
		public async Task With_Parts_Calls_Client_GetQuery_And_Db_QueryAsync()
		{
			// Arrange
			var value = F.Rnd.Str;
			var (parts, param) = DbQuery_Setup.GetParts();
			var (db, client, _, query) = DbQuery_Setup.Get(value, param);
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			await query.QueryAsync<int>(parts, transaction);

			// Assert
			client.Received().GetQuery(parts);
			await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction);
		}

		[Fact]
		public async Task With_Parts_And_Page_Calls_Client_GetCountQuery_And_Client_GetQuery_And_Db_QueryAsync()
		{
			// Arrange
			var value = F.Rnd.Str;
			var (parts, param) = DbQuery_Setup.GetParts();
			var (db, client, _, query) = DbQuery_Setup.Get(value, param);
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			await query.QueryAsync<int>(F.Rnd.Lng, parts, transaction);

			// Assert
			client.Received().GetCountQuery(parts);
			await db.Received().ExecuteAsync<long>(value, param, CommandType.Text, transaction);
			client.Received().GetQuery(Arg.Any<IQueryParts>());
			await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction);
		}

		[Fact]
		public async Task Logs_Query_To_Debug()
		{
			// Arrange
			var value = F.Rnd.Str;
			var (parts, param) = DbQuery_Setup.GetParts();
			var (_, _, log, query) = DbQuery_Setup.Get(value, param);
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			await query.QueryAsync<int>(value, param, transaction);
			await query.QueryAsync<int>(value, param, CommandType.StoredProcedure, transaction);
			await query.QueryAsync<int>(parts, transaction);
			await query.QueryAsync<int>(F.Rnd.Lng, parts, transaction);

			// Assert
			log.ReceivedWithAnyArgs(4).Debug(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}
