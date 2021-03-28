// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbFunc_Tests
{
	public class CreateAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetCreateQuery()
		{
			// Arrange
			var (client, _, func) = DbFunc_Setup.Get();
			var entity = new DbFunc_Setup.Foo { Id = new(F.Rnd.Int) };

			// Act
			await func.CreateAsync(entity);

			// Assert
			client.Received().GetCreateQuery<DbFunc_Setup.Foo>();
		}

		[Fact]
		public async Task Logs_Query_To_Debug()
		{
			// Arrange
			var (_, log, func) = DbFunc_Setup.Get();
			var entity = new DbFunc_Setup.Foo { Id = new(F.Rnd.Int) };

			// Act
			await func.CreateAsync(entity);

			// Assert
			log.ReceivedWithAnyArgs().Debug(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}
