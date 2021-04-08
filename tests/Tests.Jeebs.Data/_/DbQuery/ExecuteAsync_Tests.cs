// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbQuery_Tests
{
	public class ExecuteAsync_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.TableDirect)]
		[InlineData(CommandType.Text)]
		public async Task Calls_Db_ExecuteAsync_With_CommandType(CommandType input)
		{
			// Arrange
			var (db, _, _, query) = DbQuery_Setup.Get();
			var value = F.Rnd.Str;
			var param = F.Rnd.Int;
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			await query.ExecuteAsync(value, param, input, transaction);
			await query.ExecuteAsync<long>(value, param, input, transaction);

			// Assert
			await db.Received().ExecuteAsync(value, param, input, transaction);
			await db.Received().ExecuteAsync<long>(value, param, input, transaction);
		}

		[Fact]
		public async Task Calls_Db_ExecuteAsync_As_Text()
		{
			// Arrange
			var (db, _, _, query) = DbQuery_Setup.Get();
			var value = F.Rnd.Str;
			var param = F.Rnd.Int;
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			await query.ExecuteAsync(value, param, transaction);
			await query.ExecuteAsync<long>(value, param, transaction);

			// Assert
			await db.Received().ExecuteAsync(value, param, CommandType.Text, transaction);
			await db.Received().ExecuteAsync<long>(value, param, CommandType.Text, transaction);
		}

		[Fact]
		public async Task Logs_Query_To_Debug()
		{
			// Arrange
			var value = F.Rnd.Str;
			var (_, param) = DbQuery_Setup.GetParts();
			var (_, _, log, query) = DbQuery_Setup.Get(value, param);
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			await query.ExecuteAsync(value, param, transaction);
			await query.ExecuteAsync(value, param, CommandType.StoredProcedure, transaction);
			await query.ExecuteAsync<long>(value, param, transaction);
			await query.ExecuteAsync<long>(value, param, CommandType.TableDirect, transaction);

			// Assert
			log.ReceivedWithAnyArgs(4).Debug(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}
