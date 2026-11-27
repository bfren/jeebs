// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Common;

namespace Jeebs.Data.Clients.PostgreSql.PostgreSqlDbClient_Tests;

public class Constructor_Tests
{
	[Fact]
	public async Task With_Cache__Queries_Use_Cache()
	{
		// Arrange
		var cache = Substitute.For<ICacheService>();
		var connection = Substitute.For<IDbConnection>();
		_ = connection.ConnectionString.Returns(Rnd.Str);

		// Act
		var client = new PostgreSqlDbClient(cache);
		_ = await client.Adapter.QueryAsync<string>(connection, null, Rnd.Str, null, CommandType.Text);

		// Assert
		_ = Assert.IsType<PostgreSqlDbTypeMapper>(client.Adapter.Mapper);
		_ = await cache.Received(1).GetOrCreateAsync(Arg.Any<string>(), Arg.Any<object?>(),
			Arg.Any<Func<Task<IEnumerable<string>>>>());
	}
}
