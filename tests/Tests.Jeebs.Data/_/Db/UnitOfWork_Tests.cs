// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests;

public class UnitOfWork_Tests
{
	[Fact]
	public void Calls_Client_Connect()
	{
		// Arrange
		var (_, _, client, _, db) = Db_Setup.Get();

		// Act
		_ = db.UnitOfWork;

		// Assert
		client.Received().Connect(Arg.Any<string>());
	}

	[Fact]
	public void Calls_Connection_BeginTransaction()
	{
		// Arrange
		var (_, _, _, connection, db) = Db_Setup.Get();

		// Act
		_ = db.UnitOfWork;

		// Assert
		connection.Received().BeginTransaction();
	}
}
