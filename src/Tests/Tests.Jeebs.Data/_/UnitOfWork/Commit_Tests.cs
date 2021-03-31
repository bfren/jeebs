// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class Commit_Tests
	{
		[Fact]
		public void Logs_Action_And_Calls_Transaction_Commit()
		{
			// Arrange
			var transaction = Substitute.For<IDbTransaction>();
			var log = Substitute.For<ILog>();
			var unitOfWork = new UnitOfWork(transaction, log);

			// Act
			unitOfWork.Commit();

			// Assert
			transaction.Received().Commit();
			log.Received().Debug("Committing transaction.");
		}

		[Fact]
		public void Commits_Only_Once()
		{
			// Arrange
			var transaction = Substitute.For<IDbTransaction>();
			var log = Substitute.For<ILog>();
			var unitOfWork = new UnitOfWork(transaction, log);

			// Act
			unitOfWork.Commit();
			unitOfWork.Commit();
			unitOfWork.Commit();

			// Assert
			transaction.Received(1).Commit();
		}

		[Fact]
		public void Does_Not_Commit_If_Rolled_Back()
		{
			// Arrange
			var transaction = Substitute.For<IDbTransaction>();
			var log = Substitute.For<ILog>();
			var unitOfWork = new UnitOfWork(transaction, log);

			// Act
			unitOfWork.Rollback();
			unitOfWork.Commit();

			// Assert
			transaction.DidNotReceive().Commit();
		}

		[Fact]
		public void Catches_Exception_And_Logs_Error()
		{
			// Arrange
			var transaction = Substitute.For<IDbTransaction>();
			var exception = new Exception();
			transaction.When(t => t.Commit()).Throw(exception);
			var log = Substitute.For<ILog>();
			var unitOfWork = new UnitOfWork(transaction, log);

			// Act
			unitOfWork.Commit();

			// Assert
			log.Received().Error(exception, "Error committing transaction.");
		}
	}
}
