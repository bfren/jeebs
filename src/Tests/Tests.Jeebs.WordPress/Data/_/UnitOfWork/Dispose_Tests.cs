// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.WordPress.Data.UnitOfWork_Tests
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
