// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jx.Config;
using Xunit;

namespace Jeebs.Config.DbConfig_Tests
{
	public class GetConnection_Tests
	{
		[Fact]
		public void Default_Not_Set_Throws_DefaultDbConnectionUndefinedException()
		{
			// Arrange
			var config = new DbConfig();

			// Act
			void action() => config.GetConnection();

			// Assert
			var ex = Assert.Throws<DefaultDbConnectionUndefinedException>(action);
			Assert.Equal("Default database connection is not defined.", ex.Message);
		}

		[Fact]
		public void No_Connections_Throws_NoDbConnectionsException()
		{
			// Arrange
			var config = new DbConfig { Default = F.Rnd.Str };

			// Act
			void action() => config.GetConnection();

			// Assert
			var ex = Assert.Throws<NoDbConnectionsException>(action);
			Assert.Equal("At least one database connection must be defined.", ex.Message);
		}

		[Fact]
		public void Connection_Not_Found_Throws_NamedDbConnectionNotFoundException()
		{
			// Arrange
			var name = F.Rnd.Str;
			var config = new DbConfig { Default = name };
			config.Connections.Add(F.Rnd.Str, new DbConnectionConfig());

			// Act
			void action() => config.GetConnection();

			// Assert
			var ex = Assert.Throws<NamedDbConnectionNotFoundException>(action);
			Assert.Equal(string.Format(NamedDbConnectionNotFoundException.Format, name), ex.Message);
		}

		[Fact]
		public void Connection_Not_Specified_Returns_Default_Connection()
		{
			// Arrange
			var name = F.Rnd.Str;
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
			var name = F.Rnd.Str;
			var connection = new DbConnectionConfig();
			var config = new DbConfig { Default = F.Rnd.Str };
			config.Connections.Add(name, connection);

			// Act
			var result = config.GetConnection(name);

			// Assert
			Assert.Equal(connection, result);
		}
	}
}
