// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Common;

namespace Jeebs.Data.Adapters.Dapper.DapperAdapter_Tests;

public class QuerySingleAsync_Tests
{
	[Fact]
	public async Task Without_Transaction__Uses_Cache()
	{
		// Arrange
		var connection = Substitute.For<IDbConnection>();
		_ = connection.ConnectionString.Returns(Rnd.Str);
		var cache = Substitute.For<ICacheService>();
		var adapter = new DapperAdapter(cache: cache);

		// Act
		_ = await adapter.QuerySingleAsync<string>(connection, null, Rnd.Str, null, CommandType.Text);

		// Assert
		_ = await cache.ReceivedWithAnyArgs(1).GetOrCreateAsync<string>(default!, default, default!);
	}

	[Fact]
	public async Task With_Transaction__Does_Not_Use_Cache()
	{
		// Arrange
		var connection = Substitute.For<IDbConnection>();
		var cache = Substitute.For<ICacheService>();
		var adapter = new DapperAdapter(cache: cache);

		// Act
		_ = await adapter.QuerySingleAsync<string>(connection, Substitute.For<IDbTransaction>(), Rnd.Str, null, CommandType.Text);

		// Assert
		_ = await cache.DidNotReceiveWithAnyArgs().GetOrCreateAsync<string>(default!, default, default!);
	}
}
