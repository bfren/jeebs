// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Db.DbConfig_Tests;

public class GetConnection_Tests
{
	[Fact]
	public void Default_Not_Set_Returns_Fail()
	{
		// Arrange
		var config = new DbConfig();

		// Act
		var result = config.GetConnection();

		// Assert
		result.AssertFail("Default database connection is not defined.");
	}

	[Fact]
	public void No_Connections_Returns_Fail()
	{
		// Arrange
		var config = new DbConfig { Default = Rnd.Str };

		// Act
		var result = config.GetConnection();

		// Assert
		result.AssertFail("At least one database connection must be defined.");
	}

	[Fact]
	public void Connection_Not_Found_Throws_NamedDbConnectionNotFoundException()
	{
		// Arrange
		var name = Rnd.Str;
		var config = new DbConfig { Default = name };
		config.Connections.Add(Rnd.Str, new DbConnectionConfig());

		// Act
		var result = config.GetConnection();

		// Assert
		result.AssertFail("A connection named '{Name}' could not be found.", new { name });
	}

	[Fact]
	public void Connection_Not_Specified_Returns_Default_Connection()
	{
		// Arrange
		var name = Rnd.Str;
		var connection = new DbConnectionConfig();
		var config = new DbConfig { Default = name };
		config.Connections.Add(name, connection);

		// Act
		var result = config.GetConnection();

		// Assert
		Assert.Equal(connection, result);
	}

	[Fact]
	public void Returns_Named_Connection()
	{
		// Arrange
		var name = Rnd.Str;
		var connection = new DbConnectionConfig();
		var config = new DbConfig { Default = Rnd.Str };
		config.Connections.Add(name, connection);

		// Act
		var result = config.GetConnection(name);

		// Assert
		Assert.Equal(connection, result);
	}
}
