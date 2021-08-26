// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Linq.Expressions;
using Jeebs.Data.Enums;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Repository_Tests
{
	public class QueryAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetQuery()
		{
			// Arrange
			var (client, _, repo) = Repository_Setup.Get();
			var predicates = new List<(Expression<Func<Repository_Setup.Foo, object>>, Compare, object)>
			{
				(f => f.Id, Compare.NotEqual, F.Rnd.Int)
			}.ToArray();

			// Act
			await repo.QueryAsync<Repository_Setup.FooModel>(predicates);

			// Assert
			client.Received().GetQuery<Repository_Setup.Foo, Repository_Setup.FooModel>(predicates);
		}

		[Fact]
		public async Task Logs_Query_To_Debug()
		{
			// Arrange
			var (_, log, repo) = Repository_Setup.Get();
			var predicates = new List<(Expression<Func<Repository_Setup.Foo, object>>, Compare, object)>
			{
				(f => f.Id, Compare.NotEqual, F.Rnd.Int)
			}.ToArray();

			// Act
			await repo.QueryAsync<Repository_Setup.FooModel>(predicates);

			// Assert
			log.ReceivedWithAnyArgs().Debug(Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}
