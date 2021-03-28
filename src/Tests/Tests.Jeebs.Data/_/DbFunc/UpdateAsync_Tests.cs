// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbFunc_Tests
{
	public class UpdateAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetUpdateQuery()
		{
			// Arrange
			var (client, _, func) = DbFunc_Setup.Get();
			var value = F.Rnd.Lng;
			var model = new DbFunc_Setup.FooModel { Id = new(value) };

			// Act
			await func.UpdateAsync(model);

			// Assert
			client.Received().GetUpdateQuery<DbFunc_Setup.Foo, DbFunc_Setup.FooModel>(value);
		}

		[Fact]
		public async Task Logs_Query_To_Debug()
		{
			// Arrange
			var (_, log, func) = DbFunc_Setup.Get();
			var value = F.Rnd.Lng;
			var model = new DbFunc_Setup.FooModel { Id = new(value) };

			// Act
			await func.UpdateAsync(model);

			// Assert
			log.ReceivedWithAnyArgs().Debug(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}
