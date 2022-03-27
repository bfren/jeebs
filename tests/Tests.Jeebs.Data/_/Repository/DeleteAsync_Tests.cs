// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository_Tests;

public class DeleteAsync_Tests
{
	[Fact]
	public async Task Calls_Client_GetDeleteQuery()
	{
		// Arrange
		var (client, _, repo) = Repository_Setup.Get();
		var value = Rnd.Lng;
		var model = new Repository_Setup.FooModel { Id = new() { Value = value } };

		// Act
		await repo.DeleteAsync(model);

		// Assert
		client.Received().GetDeleteQuery<Repository_Setup.Foo>(value);
	}

	[Fact]
	public async Task Logs_Query_To_Debug()
	{
		// Arrange
		var (_, log, repo) = Repository_Setup.Get();
		var value = Rnd.Lng;
		var model = new Repository_Setup.FooModel { Id = new() { Value = value } };

		// Act
		await repo.DeleteAsync(model);

		// Assert
		log.ReceivedWithAnyArgs().Dbg(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
