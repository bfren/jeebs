// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Adapters.Dapper.DapperAdapter_Tests;

public class CacheKey_Tests
{
	private static IDbConnection Connection(string connectionString)
	{
		var connection = Substitute.For<IDbConnection>();
		_ = connection.ConnectionString.Returns(connectionString);
		return connection;
	}

	[Fact]
	public void Same_Connection_String__Returns_Same_Key()
	{
		// Arrange
		var connectionString = Rnd.Str;
		var query = Rnd.Str;

		// Act
		var k0 = DapperAdapter.CacheKey(Connection(connectionString), query);
		var k1 = DapperAdapter.CacheKey(Connection(connectionString), query);

		// Assert
		Assert.Equal(k0, k1);
	}

	[Fact]
	public void Different_Connection_Strings__Return_Different_Keys()
	{
		// Arrange
		var query = Rnd.Str;

		// Act
		var k0 = DapperAdapter.CacheKey(Connection(Rnd.Str), query);
		var k1 = DapperAdapter.CacheKey(Connection(Rnd.Str), query);

		// Assert
		Assert.NotEqual(k0, k1);
	}

	[Fact]
	public void Does_Not_Contain_Connection_String()
	{
		// Arrange
		var password = Rnd.Str;
		var query = Rnd.Str;

		// Act
		var result = DapperAdapter.CacheKey(Connection($"Server=db;Password={password}"), query);

		// Assert
		Assert.DoesNotContain(password, result);
		Assert.EndsWith($":{query}", result);
	}
}
