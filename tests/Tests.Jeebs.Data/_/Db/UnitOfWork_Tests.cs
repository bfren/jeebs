// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests
{
	public class UnitOfWork_Tests
	{
		[Fact]
		public void Calls_Connection_BeginTransaction()
		{
			// Arrange
			var (_, _, _, connection, db) = Db_Setup.Get();

			// Act
			var _ = db.UnitOfWork;

			// Assert
			connection.Received().BeginTransaction();
		}
	}
}
