// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;
using Jeebs.Logging;

namespace Jeebs.Data.UnitOfWork_Tests;

public class Rollback_Tests
{
	[Fact]
	public void Logs_Action_And_Calls_Transaction_Commit()
	{
		// Arrange
		var transaction = Substitute.For<DbTransaction>();
		var connection = Substitute.For<DbConnection>();
		var log = Substitute.For<ILog>();
		var unitOfWork = new UnitOfWork(connection, transaction, log);

		// Act
		unitOfWork.Rollback();

		// Assert
		transaction.Received().Rollback();
		log.Received().Dbg("Rolling back transaction.");
	}

	[Fact]
	public void Rolls_Back_Only_Once()
	{
		// Arrange
		var transaction = Substitute.For<DbTransaction>();
		var connection = Substitute.For<DbConnection>();
		var log = Substitute.For<ILog>();
		var unitOfWork = new UnitOfWork(connection, transaction, log);

		// Act
		unitOfWork.Rollback();
		unitOfWork.Rollback();
		unitOfWork.Rollback();

		// Assert
		transaction.Received(1).Rollback();
	}

	[Fact]
	public void Does_Not_Roll_Back_If_Committed()
	{
		// Arrange
		var transaction = Substitute.For<DbTransaction>();
		var connection = Substitute.For<DbConnection>();
		var log = Substitute.For<ILog>();
		var unitOfWork = new UnitOfWork(connection, transaction, log);

		// Act
		unitOfWork.Commit();
		unitOfWork.Rollback();

		// Assert
		transaction.DidNotReceive().Rollback();
	}

	[Fact]
	public void Catches_Exception_And_Logs_Error()
	{
		// Arrange
		var transaction = Substitute.For<DbTransaction>();
		var connection = Substitute.For<DbConnection>();
		var exception = new Exception();
		transaction.When(t => t.Rollback()).Throw(exception);
		var log = Substitute.For<ILog>();
		var unitOfWork = new UnitOfWork(connection, transaction, log);

		// Act
		unitOfWork.Rollback();

		// Assert
		log.Received().Err(exception, "Error rolling back transaction.");
	}
}
