// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Common;

namespace Jeebs.Data.Adapters.Dapper.DapperAdapter_Tests;

public class QueryAsync_Tests
{
	[Fact]
	public async Task Returns_Cached_Value_Using_Connection_Key()
	{
		// Arrange
		var connection = Substitute.For<IDbConnection>();
		_ = connection.ConnectionString.Returns(Rnd.Str);
		var query = Rnd.Str;
		var param = new { Id = Rnd.Int };
		var expected = new[] { Rnd.Str, Rnd.Str };
		var cache = Substitute.For<ICacheService>();
		_ = cache.GetOrCreateAsync(Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<Func<Task<IEnumerable<string>>>>())
			.Returns(expected);
		var adapter = new DapperAdapter(DapperTypeMapper.Instance, cache);

		// Act
		var result = await adapter.QueryAsync<string>(connection, null, query, param, CommandType.Text);

		// Assert
		var ok = result.AssertOk();
		Assert.Same(expected, ok);
		_ = await cache.Received(1).GetOrCreateAsync(DapperAdapter.CacheKey(connection, query), param,
			Arg.Any<Func<Task<IEnumerable<string>>>>());
	}

	[Fact]
	public async Task Query_Throws__Failure_Does_Not_Include_Parameters()
	{
		// Arrange
		var connection = Substitute.For<IDbConnection>();
		_ = connection.ConnectionString.Returns(Rnd.Str);
		var query = Rnd.Str;
		var param = new { PasswordHash = Rnd.Str };
		var cache = Substitute.For<ICacheService>();
		_ = cache.GetOrCreateAsync(Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<Func<Task<IEnumerable<string>>>>())
			.Returns<Task<IEnumerable<string>>>(_ => throw new InvalidOperationException());
		var adapter = new DapperAdapter(cache: cache);

		// Act
		var result = await adapter.QueryAsync<string>(connection, null, query, param, CommandType.Text);

		// Assert
		_ = result.AssertFailure("Error executing query '{Query}'.", query);
	}

	[Fact]
	public async Task With_Transaction__Does_Not_Use_Cache()
	{
		// Arrange
		var connection = Substitute.For<IDbConnection>();
		var cache = Substitute.For<ICacheService>();
		var adapter = new DapperAdapter(cache: cache);

		// Act
		_ = await adapter.QueryAsync<string>(connection, Substitute.For<IDbTransaction>(), Rnd.Str, null, CommandType.Text);

		// Assert
		_ = await cache.DidNotReceiveWithAnyArgs().GetOrCreateAsync<IEnumerable<string>>(default!, default, default!);
	}
}
