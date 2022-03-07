// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Enums;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Repository_Tests;

public class QuerySingleAsync_Tests
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
		_ = await repo.QuerySingleAsync<Repository_Setup.FooModel>(predicates).ConfigureAwait(false);

		// Assert
		_ = client.Received().GetQuery<Repository_Setup.Foo, Repository_Setup.FooModel>(predicates);
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
		_ = await repo.QuerySingleAsync<Repository_Setup.FooModel>(predicates).ConfigureAwait(false);

		// Assert
		log.ReceivedWithAnyArgs().Dbg(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
