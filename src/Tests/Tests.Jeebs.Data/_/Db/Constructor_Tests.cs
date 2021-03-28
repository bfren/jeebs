// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties()
		{
			// Arrange

			// Act
			var (config, _, client, _, db) = Db_Setup.Get();

			// Assert
			Assert.Same(client, db.Client);
			Assert.Same(config, db.Config);
		}

		[Fact]
		public void Attempts_To_Connect_To_Database()
		{
			// Arrange

			// Act
			var (config, _, client, _, _) = Db_Setup.Get();

			// Assert
			client.Received().Connect(config.ConnectionString);
		}
	}
}
