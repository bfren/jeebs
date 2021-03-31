// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Repository_Tests
{
	public class CreateAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetCreateQuery()
		{
			// Arrange
			var (client, _, repo) = Repository_Setup.Get();
			var foo = new Repository_Setup.Foo { Id = new(F.Rnd.Int) };

			// Act
			await repo.CreateAsync(foo);

			// Assert
			client.Received().GetCreateQuery<Repository_Setup.Foo>();
		}

		[Fact]
		public async Task Logs_Query_To_Debug()
		{
			// Arrange
			var (_, log, repo) = Repository_Setup.Get();
			var foo = new Repository_Setup.Foo { Id = new(F.Rnd.Int) };

			// Act
			await repo.CreateAsync(foo);

			// Assert
			log.ReceivedWithAnyArgs().Debug(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}
