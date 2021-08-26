// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.Config.DbConfig_Tests
{
	public class GetAuthenticationConnection_Tests
	{
		[Fact]
		public void Returns_Default_Connection_When_Authentication_Name_Not_Set()
		{
			// Arrange
			var name = F.Rnd.Str;
			var connection = new DbConnectionConfig();
			var config = new DbConfig
			{
				Default = name,
				Connections = new Dictionary<string, DbConnectionConfig> { { name, connection } }
			};

			// Act
			var result = config.GetAuthenticationConnection();

			// Assert
			Assert.Equal(connection, result);
		}

		[Fact]
		public void Returns_Authentication_Connection_When_Authentication_Name_Set()
		{
			// Arrange
			var name = F.Rnd.Str;
			var connection = new DbConnectionConfig();
			var config = new DbConfig
			{
				Authentication = name,
				Connections = new Dictionary<string, DbConnectionConfig> { { name, connection } }
			};

			// Act
			var result = config.GetAuthenticationConnection();

			// Assert
			Assert.Equal(connection, result);
		}
	}
}
