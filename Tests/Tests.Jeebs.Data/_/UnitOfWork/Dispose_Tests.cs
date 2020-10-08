using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
