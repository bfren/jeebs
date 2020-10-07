using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NSubstitute;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class Rollback_Tests
	{
		[Fact]
		public void Calls_Transaction_Rollback()
		{
			// Arrange
			var transaction = Substitute.For<IDbTransaction>();
			var connection = Substitute.For<IDbConnection>();
			connection.BeginTransaction().Returns(transaction);

			var (w, _, _, _) = GetUnitOfWork(connection: connection);

			// Act
			w.Rollback();

			// Assert
			transaction.Received().Rollback();
		}

		[Fact]
		public void Catches_And_Logs_Commit_Exception()
		{
			// Arrange
			var exception = new Exception();

			var transaction = Substitute.For<IDbTransaction>();
			transaction.When(t => t.Rollback()).Throw(exception);

			var connection = Substitute.For<IDbConnection>();
			connection.BeginTransaction().Returns(transaction);

			var (w, _, _, log) = GetUnitOfWork(connection: connection);

			// Act
			w.Rollback();

			// Assert
			log.Received().Error(exception, "Error rolling back transaction.");
		}
	}
}
