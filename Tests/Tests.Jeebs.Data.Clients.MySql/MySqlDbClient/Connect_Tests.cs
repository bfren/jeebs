using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests
{
	public class Connect_Tests
	{
		[Fact]
		public void InvalidConnectionString_Throws_ArgumentException()
		{
			// Arrange
			var client = new MySqlDbClient();
			var connectionString = F.Rnd.String;

			// Act
			void action() => client.Connect(connectionString);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Creates_MySqlConnection_With_ConnectionString()
		{
			// Arrange
			var client = new MySqlDbClient();
			var connectionString = string.Format(
				"server={0};database={1};user id={2};password={3}",
				F.Rnd.String,
				F.Rnd.String,
				F.Rnd.String,
				F.Rnd.String
			);

			// Act
			var result = client.Connect(connectionString);

			// Assert
			Assert.Equal(connectionString, result.ConnectionString);
		}

		[Fact]
		public void Creates_New_MySqlConnection()
		{
			// Arrange
			var client = new MySqlDbClient();
			var connectionString = string.Format(
				"server={0};database={1};user id={2};password={3}",
				F.Rnd.String,
				F.Rnd.String,
				F.Rnd.String,
				F.Rnd.String
			);

			// Act
			var c0 = client.Connect(connectionString);
			var c1 = client.Connect(connectionString);

			// Assert
			Assert.NotEqual(c0, c1);
		}
	}
}
