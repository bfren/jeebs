// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class Dispose_Tests
	{
		[Fact]
		public void Calls_Transaction_Commit()
		{
			// Arrange
			var transaction = Substitute.For<IDbTransaction>();
			var log = Substitute.For<ILog>();
			var unitOfWork = new UnitOfWork(transaction, log);

			// Act
			unitOfWork.Dispose();

			// Assert
			transaction.Received().Commit();
		}
	}
}
