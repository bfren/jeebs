// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class Dispose_Tests
	{
		[Fact]
		public void Commits_Then_Disposes_Transaction_And_Connection()
		{
			// Arrange
			var (w, connection, transaction, _, _, _) = GetUnitOfWork();

			// Act
			w.Dispose();

			// Assert
			transaction.Received().Commit();
			transaction.Received().Dispose();
			connection.Received().Dispose();
		}
	}
}
