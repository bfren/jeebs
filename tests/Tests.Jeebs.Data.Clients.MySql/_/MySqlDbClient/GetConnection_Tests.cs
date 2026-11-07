// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MySqlConnector;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests;

public class GetConnection_Tests : MySqlDbClient_Setup
{
	[Fact]
	public void Returns_New_MySqlConnection_With_Connection_String()
	{
		// Arrange
		var (client, _) = Setup();
		var connectionString = $"Server={Rnd.Str};Database={Rnd.Str}";

		// Act
		var c0 = client.GetConnection(connectionString);
		var c1 = client.GetConnection(connectionString);

		// Assert
		var mysql = Assert.IsType<MySqlConnection>(c0);
		Assert.Equal(new MySqlConnectionStringBuilder(connectionString).ConnectionString, mysql.ConnectionString);
		Assert.NotSame(c0, c1);
	}
}
