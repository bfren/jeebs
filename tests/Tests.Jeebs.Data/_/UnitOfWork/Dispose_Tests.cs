// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.UnitOfWork_Tests;

public class Dispose_Tests
{
	[Fact]
	public void Calls_Transaction_Commit()
	{
		// Arrange
		var transaction = Substitute.For<IDbTransaction>();
		var connection = Substitute.For<IDbConnection>();
		_ = connection.BeginTransaction().Returns(transaction);
		var log = Substitute.For<ILog>();
		var unitOfWork = new UnitOfWork(connection, log);

		// Act
		unitOfWork.Dispose();

		// Assert
		transaction.Received().Commit();
	}
}
