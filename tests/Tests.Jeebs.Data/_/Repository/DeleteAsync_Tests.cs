// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Repository_Tests;

public class DeleteAsync_Tests
{
	[Fact]
	public async Task Calls_Client_GetDeleteQuery()
	{
		// Arrange
		var (client, _, repo) = Repository_Setup.Get();
		var value = F.Rnd.Ulng;

		// Act
		await repo.DeleteAsync(new Repository_Setup.FooId(value));

		// Assert
		client.Received().GetDeleteQuery<Repository_Setup.Foo>(value);
	}

	[Fact]
	public async Task Logs_Query_To_Debug()
	{
		// Arrange
		var (_, log, repo) = Repository_Setup.Get();
		var value = F.Rnd.Ulng;

		// Act
		await repo.DeleteAsync(new Repository_Setup.FooId(value));

		// Assert
		log.ReceivedWithAnyArgs().Debug(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
