// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Config;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Jeebs.Data.Db_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties()
		{
			// Arrange
			var config = new DbConnectionConfig();
			var log = Substitute.For<ILog>();
			var client = Substitute.For<IDbClient>();
			var name = F.Rnd.Str;

			// Act
			var result = Substitute.ForPartsOf<Db>(config, log, client, name);

			// Assert
			Assert.Same(client, result.Client);
			Assert.Same(config, result.Config);
		}

		[Fact]
		public void Attempts_To_Connect_To_Database()
		{
			// Arrange
			var value = F.Rnd.Str;
			var config = new DbConnectionConfig { ConnectionString = value };
			var log = Substitute.For<ILog>();
			var client = Substitute.For<IDbClient>();
			var name = F.Rnd.Str;

			// Act
			Substitute.ForPartsOf<Db>(config, log, client, name);

			// Assert
			client.Received().Connect(value);
		}

		[Fact]
		public void Logs_Failure_To_Connect_To_Database()
		{
			// Arrange
			var value = F.Rnd.Str;
			var config = new DbConnectionConfig { ConnectionString = value };
			var log = Substitute.For<ILog>();
			var client = Substitute.For<IDbClient>();
			var exception = new Exception(F.Rnd.Str);
			client.Connect(value).Throws(exception);
			var name = F.Rnd.Str;

			// Act
			Substitute.ForPartsOf<Db>(config, log, client, name);

			// Assert
			log.Received().Fatal(exception, "Unable to connect to database {Name}", name);
		}
	}
}
