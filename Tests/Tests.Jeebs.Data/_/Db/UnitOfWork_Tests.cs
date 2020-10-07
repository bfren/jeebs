using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests
{
	public class UnitOfWork_Tests
	{
		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		public void Without_ConnectionString_Throws_ConnectionException(string connectionString)
		{
			// Arrange
			var client = Substitute.For<IDbClient>();
			var log = Substitute.For<ILog>();
			var db = Substitute.ForPartsOf<Db>(client, log, connectionString);

			// Act
			IUnitOfWork action() => db.UnitOfWork;

			// Assert
			Assert.Throws<Jx.Data.ConnectionException>(action);
		}

		[Fact]
		public void With_ConnectionString_Calls_Client_Connect()
		{
			// Arrange
			var client = Substitute.For<IDbClient>();
			var log = Substitute.For<ILog>();
			var connectionString = F.StringF.Random(6);
			var db = Substitute.ForPartsOf<Db>(client, log, connectionString);

			// Act
			_ = db.UnitOfWork;

			// Assert
			client.Received().Connect(connectionString);
		}

		[Theory]
		[InlineData(ConnectionState.Broken)]
		[InlineData(ConnectionState.Closed)]
		[InlineData(ConnectionState.Connecting)]
		[InlineData(ConnectionState.Executing)]
		[InlineData(ConnectionState.Fetching)]
		public void If_ConnectionState_Not_Open_Calls_Connection_Open(ConnectionState state)
		{
			// Arrange
			var connection = Substitute.For<IDbConnection>();
			connection.State.Returns(state);

			var client = Substitute.For<IDbClient>();
			client.Connect(Arg.Any<string>()).Returns(connection);

			var log = Substitute.For<ILog>();
			var connectionString = F.StringF.Random(6);

			var db = Substitute.ForPartsOf<Db>(client, log, connectionString);

			// Act
			_ = db.UnitOfWork;

			// Assert
			connection.Received().Open();
		}

		[Fact]
		public void If_ConnectionState_Open_DoesNotCall_Connection_Open()
		{
			// Arrange
			var connection = Substitute.For<IDbConnection>();
			connection.State.Returns(ConnectionState.Open);

			var client = Substitute.For<IDbClient>();
			client.Connect(Arg.Any<string>()).Returns(connection);

			var log = Substitute.For<ILog>();
			var connectionString = F.StringF.Random(6);

			var db = Substitute.ForPartsOf<Db>(client, log, connectionString);

			// Act
			_ = db.UnitOfWork;

			// Assert
			connection.DidNotReceive().Open();
		}

		[Fact]
		public void Returns_UnitOfWork()
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

			var log = Substitute.For<ILog>();
			var connectionString = F.StringF.Random(6);

			var db = Substitute.ForPartsOf<Db>(client, log, connectionString);

			// Act
			var result = db.UnitOfWork;

			// Assert
			Assert.IsType<UnitOfWork>(result);
			Assert.Equal(adapter, result.Adapter);
			Assert.Equal(connection, result.Connection);
			Assert.Equal(transaction, result.Transaction);
		}
	}
}
