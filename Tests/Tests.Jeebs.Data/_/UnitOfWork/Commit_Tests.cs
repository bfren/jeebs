using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class Commit_Tests
	{
		[Fact]
		public void Calls_Transaction_Commit()
		{
			// Arrange
			var (w, _, transaction, _, _, _) = GetUnitOfWork();

			// Act
			w.Commit();

			// Assert
			transaction.Received().Commit();
		}

		[Fact]
		public void Catches_And_Logs_Commit_Exception()
		{
			// Arrange
			var exception = new Exception();

			var transaction = Substitute.For<IDbTransaction>();
			transaction.When(t => t.Commit()).Throw(exception);

			var (w, _, _, _, log, _) = GetUnitOfWork(transaction: transaction);

			// Act
			w.Commit();

			// Assert
			log.Received().Error(exception, "Error committing transaction.");
		}
	}
}
