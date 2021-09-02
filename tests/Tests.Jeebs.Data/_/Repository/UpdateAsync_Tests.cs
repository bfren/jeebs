// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Repository_Tests
{
	public class UpdateAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetUpdateQuery()
		{
			// Arrange
			var (client, _, repo) = Repository_Setup.Get();
			var value = F.Rnd.Ulng;
			var model = new Repository_Setup.FooModel { Id = new(value) };

			// Act
			await repo.UpdateAsync(model);

			// Assert
			client.Received().GetUpdateQuery<Repository_Setup.Foo, Repository_Setup.FooModel>(value);
		}

		[Fact]
		public async Task Logs_Query_To_Debug()
		{
			// Arrange
			var (_, log, repo) = Repository_Setup.Get();
			var value = F.Rnd.Ulng;
			var model = new Repository_Setup.FooModel { Id = new(value) };

			// Act
			await repo.UpdateAsync(model);

			// Assert
			log.ReceivedWithAnyArgs().Debug(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}
