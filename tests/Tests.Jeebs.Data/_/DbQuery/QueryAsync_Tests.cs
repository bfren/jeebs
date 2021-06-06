// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Data;
using System.Threading.Tasks;
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
		public async Task WithParts_GetQuery_Exception_Returns_None_With_ErrorGettingQueryFromPartsExceptionMsg()
		{
			// Arrange
			var (parts, _) = DbQuery_Setup.GetParts();
			var (_, client, _, query) = DbQuery_Setup.Get();
			var transaction = Substitute.For<IDbTransaction>();
			client.GetQuery(parts).Throws<Exception>();

			// Act
			var result = await query.QueryAsync<int>(parts, transaction);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(none);
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
			var result = await query.QueryAsync<int>(F.Rnd.Lng, parts, transaction);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<ErrorGettingCountQueryFromPartsExceptionMsg>(none);
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
			var result = await query.QueryAsync<int>(F.Rnd.Lng, parts, transaction);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<ErrorGettingQueryFromPartsExceptionMsg>(none);
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
	}
}
