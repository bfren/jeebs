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
}
