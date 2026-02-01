// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;
using Jeebs.Logging;

namespace Jeebs.Data.Common.UnitOfWork_Tests;

public class Dispose_Tests
{
	[Fact]
	public void Calls_Transaction_Commit()
	{
		// Arrange
		var transaction = Substitute.For<DbTransaction>();
		var connection = Substitute.For<DbConnection>();
		var log = Substitute.For<ILog>();
		var unitOfWork = new UnitOfWork(connection, transaction, log);

		// Act
		unitOfWork.Dispose();

		// Assert
		transaction.Received().Commit();
	}
}
