// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
