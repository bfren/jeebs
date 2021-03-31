// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.UnitOfWork_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Properties_Set()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();

			var transaction = Substitute.For<IDbTransaction>();
			var connection = Substitute.For<IDbConnection>();
			connection.BeginTransaction().Returns(transaction);
			transaction.Connection.Returns(connection);

			var client = Substitute.For<IDbClient>();
			client.Adapter.Returns(adapter);
			client.Connect(Arg.Any<string>()).Returns(connection);

			var log = Substitute.For<ILog<Data.UnitOfWork>>();

			// Act
			var result = new Data.UnitOfWork(connection, adapter, log);

			// Assert
			Assert.Equal(adapter, result.Adapter);
			Assert.Equal(connection, result.Connection);
			Assert.Equal(transaction, result.Transaction);
		}
	}
}
