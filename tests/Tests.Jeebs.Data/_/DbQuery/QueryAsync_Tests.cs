// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using Jeebs.Data.Querying;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static Jeebs.Data.DbQuery.Msg;

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
			_ = await query.QueryAsync<int>(value, param, input);
			_ = await query.QueryAsync<int>(value, param, input, transaction);

			// Assert
			await db.Received().QueryAsync<int>(value, param, input, Arg.Any<IDbTransaction>());
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
			_ = await query.QueryAsync<int>(value, param);
			_ = await query.QueryAsync<int>(value, param, transaction);

			// Assert
			await db.Received().QueryAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
			await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction);
		}

		[Fact]
		public async Task WithParts_GetQuery_Exception_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
		{
			// Arrange
			var (parts, _) = DbQuery_Setup.GetParts();
			var (_, client, _, query) = DbQuery_Setup.Get();
			var transaction = Substitute.For<IDbTransaction>();
			client.GetQuery(parts).Throws<Exception>();

			// Act
			var r0 = await query.QueryAsync<int>(parts);
			var r1 = await query.QueryAsync<int>(parts, transaction);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(n1);
		}

		[Fact]
		public async Task WithParts_And_Page_GetCountQuery_Exception_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
		{
			// Arrange
			var (parts, _) = DbQuery_Setup.GetParts();
			var (_, client, _, query) = DbQuery_Setup.Get();
			var transaction = Substitute.For<IDbTransaction>();
			client.GetCountQuery(parts).Throws<Exception>();

			// Act
			var r0 = await query.QueryAsync<int>(F.Rnd.Ulng, parts);
			var r1 = await query.QueryAsync<int>(F.Rnd.Ulng, parts, transaction);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<ErrorGettingCountQueryFromPartsExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<ErrorGettingCountQueryFromPartsExceptionMsg>(n1);
		}

		[Fact]
		public async Task WithParts_And_Page_GetQuery_Exception_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
		{
			// Arrange
			var (parts, _) = DbQuery_Setup.GetParts();
			var (_, client, _, query) = DbQuery_Setup.Get();
			var transaction = Substitute.For<IDbTransaction>();
			client.GetQuery(Arg.Any<IQueryParts>()).ThrowsForAnyArgs<Exception>();

			// Act
			var r0 = await query.QueryAsync<int>(F.Rnd.Ulng, parts);
			var r1 = await query.QueryAsync<int>(F.Rnd.Ulng, parts, transaction);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(n1);
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
			_ = await query.QueryAsync<int>(parts);
			_ = await query.QueryAsync<int>(parts, transaction);

			// Assert
			client.Received(2).GetQuery(parts);
			await db.Received().QueryAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
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
			_ = await query.QueryAsync<int>(F.Rnd.Ulng, parts);
			_ = await query.QueryAsync<int>(F.Rnd.Ulng, parts, transaction);

			// Assert
			client.Received(2).GetCountQuery(parts);
			await db.Received().ExecuteAsync<ulong>(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
			await db.Received().ExecuteAsync<ulong>(value, param, CommandType.Text, transaction);
			client.Received(2).GetQuery(Arg.Any<IQueryParts>());
			await db.Received().QueryAsync<int>(value, param, CommandType.Text, Arg.Any<IDbTransaction>());
			await db.Received().QueryAsync<int>(value, param, CommandType.Text, transaction);
		}
	}
}
