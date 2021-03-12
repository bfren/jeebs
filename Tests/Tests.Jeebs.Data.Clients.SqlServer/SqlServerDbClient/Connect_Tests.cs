// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerDbClient_Tests
{
	public class Connect_Tests
	{
		[Fact]
		public void InvalidConnectionString_Throws_ArgumentException()
		{
			// Arrange
			var client = new SqlServerDbClient();
			var connectionString = JeebsF.Rnd.Str;

			// Act
			void action() => client.Connect(connectionString);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Creates_SqlConnection_With_ConnectionString()
		{
			// Arrange
			var client = new SqlServerDbClient();
			var connectionString = string.Format(
				"server={0};database={1};user id={2};password={3}",
				JeebsF.Rnd.Str,
				JeebsF.Rnd.Str,
				JeebsF.Rnd.Str,
				JeebsF.Rnd.Str
			);

			// Act
			var result = client.Connect(connectionString);

			// Assert
			Assert.Equal(connectionString, result.ConnectionString);
		}

		[Fact]
		public void Creates_New_SqlConnection()
		{
			// Arrange
			var client = new SqlServerDbClient();
			var connectionString = string.Format(
				"server={0};database={1};user id={2};password={3}",
				JeebsF.Rnd.Str,
				JeebsF.Rnd.Str,
				JeebsF.Rnd.Str,
				JeebsF.Rnd.Str
			);

			// Act
			var c0 = client.Connect(connectionString);
			var c1 = client.Connect(connectionString);

			// Assert
			Assert.NotEqual(c0, c1);
		}
	}
}
