// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests
{
	public class StartTransaction_Tests
	{
		[Fact]
		public void Calls_Connection_BeginTransaction()
		{
			// Arrange
			var (_, _, _, connection, db) = Db_Setup.Get();

			// Act
			var _ = db.StartTransaction;

			// Assert
			connection.Received().BeginTransaction();
		}
	}
}
