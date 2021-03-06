using System;
using NSubstitute;
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
			var (w, _, transaction, _, log, _) = GetUnitOfWork();
			var exception = new Exception();
			transaction.When(t => t.Commit()).Throw(exception);

			// Act
			w.Commit();

			// Assert
			log.Received().Error(exception, "Error committing transaction.");
		}
	}
}
